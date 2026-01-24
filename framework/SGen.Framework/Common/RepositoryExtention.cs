using Framework.Common;
using SGen.Framework.Contexts;
using SGen.Framework.Services;

namespace SGen.Framework.Common
{
    public  class RepositoryExtention
    {
        private readonly SGenDbContext _context;

        public RepositoryExtention(SGenDbContext context)
        {
            _context = context;
        }
        private  async Task AddChildEntitiesRecursive(object parentEntity, IdentityModel identity, HashSet<Type>? visited = null)
        {
            visited ??= new HashSet<Type>();
            var entityType = parentEntity.GetType();

            if (visited.Contains(entityType))
                return; // prevent circular loop
            visited.Add(entityType);

            var properties = entityType.GetProperties();

            foreach (var prop in properties)
            {
                // 1️⃣ Handle collection (one-to-many)
                if (typeof(System.Collections.IEnumerable).IsAssignableFrom(prop.PropertyType)
                    && prop.PropertyType != typeof(string))
                {
                    var children = prop.GetValue(parentEntity) as System.Collections.IEnumerable;
                    if (children == null) continue;

                    foreach (var child in children)
                    {
                        SetIdentity(child, identity);
                        await AddChildEntitiesRecursive(child, identity, visited);
                        _context.Add(child);
                    }
                }
                // 2️⃣ Handle single navigation (one-to-one or many-to-one)
                else if (!prop.PropertyType.IsPrimitive
                         && prop.PropertyType != typeof(string)
                         && prop.PropertyType.Namespace != "System")
                {
                    var child = prop.GetValue(parentEntity);
                    if (child == null) continue;

                    SetIdentity(child, identity);
                    await AddChildEntitiesRecursive(child, identity, visited);
                    _context.Add(child);
                }
            }
        }

        private void SetIdentity(object entity, IdentityModel identity, Dictionary<string, object>? foreignKeys = null)
        {
            if (entity == null) return;

            var props = entity.GetType().GetProperties();

            // 🔹 Tenant, Company, IsActive, CreatedOn auto-fill
            var tenantProp = props.FirstOrDefault(p => p.Name.Equals("TenantId", StringComparison.OrdinalIgnoreCase));
            var companyProp = props.FirstOrDefault(p => p.Name.Equals("CompanyId", StringComparison.OrdinalIgnoreCase));
            var isActiveProp = props.FirstOrDefault(p => p.Name.Equals("IsActive", StringComparison.OrdinalIgnoreCase));
            var createdOnProp = props.FirstOrDefault(p => p.Name.Equals("CreatedOn", StringComparison.OrdinalIgnoreCase));
            var modifiedOnProp = props.FirstOrDefault(p => p.Name.Equals("ModifiedOn", StringComparison.OrdinalIgnoreCase));

            tenantProp?.SetValue(entity, identity.TenantId);
            companyProp?.SetValue(entity, identity.CompanyId);
            isActiveProp?.SetValue(entity, true);

            // যদি CreatedOn null হয় (মানে নতুন entity), তাহলে assign কর
            if (createdOnProp != null && createdOnProp.GetValue(entity) == null)
                createdOnProp.SetValue(entity, DateTime.UtcNow);

            // ModifiedOn সবসময় আপডেট হবে
            modifiedOnProp?.SetValue(entity, DateTime.UtcNow);

            // 🔹 Foreign Key Mapping (যেমন POId, StoreId ইত্যাদি)
            if (foreignKeys != null && foreignKeys.Any())
            {
                foreach (var kvp in foreignKeys)
                {
                    var fkProp = props.FirstOrDefault(p => p.Name.Equals(kvp.Key, StringComparison.OrdinalIgnoreCase));
                    if (fkProp != null)
                    {
                        fkProp.SetValue(entity, kvp.Value);
                    }
                }
            }

            // 🔹 যদি entity-র ভেতরে nested collection থাকে (যেমন Details list), recursively call কর
            foreach (var prop in props)
            {
                var value = prop.GetValue(entity);
                if (value == null) continue;

                // যদি ICollection<T> হয়, মানে detail list
                if (value is System.Collections.IEnumerable enumerable && !(value is string))
                {
                    foreach (var item in enumerable)
                    {
                        if (item == null) continue;

                        // child entity তে recursive call
                        SetIdentity(item, identity, foreignKeys);
                    }
                }
            }
        }

    }
}
