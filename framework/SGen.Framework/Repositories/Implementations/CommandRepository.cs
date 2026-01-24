
using Framework.Common;
using Framework.Entities;
using Microsoft.EntityFrameworkCore;
using SGen.Framework.Contexts;
using SGen.Framework.Services;
using SGen.Framework.Services.Implementations;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace Framework.Repositories.Implementations
{
    public class CommandRepository<TEntity, TKey> :  BaseRepository<TEntity, TKey>, ICommandRepository<TEntity ,TKey>
        where TEntity : BaseEntity<TKey>
    {
        public CommandRepository(SGenDbContext context, IIdentityServices identityService)
        : base(context, identityService) { }
        public async Task AddAsync(TEntity entity)
        {
            var identity = _identityService.GetIdentity();
            identity.TenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");//identity.TenantId;
            identity.CompanyId = Guid.Parse("22222222-2222-2222-2222-222222222222");//identity.CompanyId;

            var properties = typeof(TEntity).GetProperties();

            // 3️⃣ Detect primary key property dynamically
            var tenantProp = properties.FirstOrDefault(p => p.Name.Equals("TenantId", StringComparison.OrdinalIgnoreCase));

            // ২️⃣ CompanyId
            var companyProp = properties.FirstOrDefault(p => p.Name.Equals("CompanyId", StringComparison.OrdinalIgnoreCase));

            // ৩️⃣ Main Id (Key property)
            var idProp = properties.FirstOrDefault(p =>
                Attribute.IsDefined(p, typeof(KeyAttribute)) ||
                p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) ||
                p.Name.EndsWith("Id", StringComparison.OrdinalIgnoreCase) && p != tenantProp && p != companyProp);

            if (properties == null)
                throw new Exception($"No primary key detected on {typeof(TEntity).Name}");

            // 4️⃣ Generate next PK
            int newPk = await _identityService.GenerateNextPk<TEntity>(identity.TenantId, identity.CompanyId);

            // 5️⃣ Set PK safely
            tenantProp?.SetValue(entity, Convert.ChangeType(identity.TenantId, tenantProp.PropertyType));
            companyProp?.SetValue(entity, Convert.ChangeType(identity.CompanyId, companyProp.PropertyType));
            idProp?.SetValue(entity, Convert.ChangeType(newPk, idProp.PropertyType));
            entity.IsActive = true;
            entity.CreatedBy = "Admin";
            entity.CreatedOn = DateTime.UtcNow;

            // 6️⃣ Add entity to DbSet
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            //var identity = _identityService.GetIdentity();

            //entity.TenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");//identity.TenantId;
            //entity.CompanyId = Guid.Parse("22222222-2222-2222-2222-222222222222");//identity.CompanyId;

            //// ১️⃣ Get all properties
            //var properties = typeof(TEntity).GetProperties();

            //// ২️⃣ Detect primary key property
            //var tenantProp = properties.FirstOrDefault(p => p.Name.Equals("TenantId", StringComparison.OrdinalIgnoreCase));
            //var companyProp = properties.FirstOrDefault(p => p.Name.Equals("CompanyId", StringComparison.OrdinalIgnoreCase));
            //var idProp = properties.FirstOrDefault(p =>
            //    Attribute.IsDefined(p, typeof(KeyAttribute)) ||
            //    p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) ||
            //    (p.Name.EndsWith("Id", StringComparison.OrdinalIgnoreCase) && p != tenantProp && p != companyProp));

            //if (idProp == null)
            //    throw new Exception($"No primary key detected on {typeof(TEntity).Name}");

            //// ৩️⃣ Get PK value from input entity
            //var pkValue = idProp.GetValue(entity);
            //if (pkValue == null)
            //    throw new Exception("Primary key value is null");

            //// ৪️⃣ Fetch existing entity by TenantId + CompanyId + PK
            //var existingEntity = await _dbSet
            //    .Where(e =>
            //        EF.Property<Guid>(e, tenantProp!.Name) == entity.TenantId &&
            //        EF.Property<Guid>(e, companyProp!.Name) == entity.CompanyId &&
            //        EF.Property<object>(e, idProp.Name).Equals(pkValue)
            //    )
            //    .FirstOrDefaultAsync();

            //if (existingEntity == null)
            //    throw new Exception("Entity not found for update");

            //// ৫️⃣ Copy all properties except TenantId, CompanyId, PK
            //foreach (var prop in properties)
            //{
            //    if (prop.Name == idProp.Name || prop.Name == tenantProp?.Name || prop.Name == companyProp?.Name)
            //        continue;

            //    var newValue = prop.GetValue(entity);
            //    prop.SetValue(existingEntity, newValue);
            //}

            //existingEntity.IsActive = true;
            //existingEntity.UpdatedOn = DateTime.UtcNow;

            //await _context.SaveChangesAsync();
            var identity = _identityService.GetIdentity();

            identity.TenantId = Guid.Parse("11111111-1111-1111-1111-111111111111"); //identity.TenantId;
            identity.CompanyId = Guid.Parse("22222222-2222-2222-2222-222222222222"); //identity.CompanyId;

            // Get all properties
            var properties = typeof(TEntity).GetProperties();

            // Detect PK property (except TenantId & CompanyId)
            var tenantProp = properties.FirstOrDefault(p => p.Name.Equals("TenantId", StringComparison.OrdinalIgnoreCase));
            var companyProp = properties.FirstOrDefault(p => p.Name.Equals("CompanyId", StringComparison.OrdinalIgnoreCase));
            var idProp = properties.FirstOrDefault(p =>
                Attribute.IsDefined(p, typeof(KeyAttribute)) ||
                p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) ||
                (p.Name.EndsWith("Id", StringComparison.OrdinalIgnoreCase) && p != tenantProp && p != companyProp));

            if (idProp == null)
                throw new Exception($"No primary key detected on {typeof(TEntity).Name}");

            var pkValue = idProp.GetValue(entity);
            if (pkValue == null)
                throw new Exception("Primary key value is null");

            // Fetch existing entity (TenantId + CompanyId + PK)
            var existingEntity = await _dbSet
                .Where(e =>
                    EF.Property<Guid>(e, tenantProp!.Name) == identity.TenantId &&
                    EF.Property<Guid>(e, companyProp!.Name) == identity.CompanyId &&
                    EF.Property<object>(e, idProp.Name).Equals(pkValue)
                )
                .FirstOrDefaultAsync();

            if (existingEntity == null)
                throw new Exception("Entity not found for update");

            // 🔥 Replace manual loop → EF built-in SetValues
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);

            // Exclude PK + TenantId + CompanyId from modification
            _context.Entry(existingEntity).Property("TenantId").IsModified = false;
            _context.Entry(existingEntity).Property("CompanyId").IsModified = false;
            _context.Entry(existingEntity).Property(idProp.Name).IsModified = false;

            // Audit fields
            existingEntity. IsActive = true;
            existingEntity.UpdatedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var identity = _identityService.GetIdentity();

            identity.TenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            identity.CompanyId = Guid.Parse("22222222-2222-2222-2222-222222222222");

            var properties = typeof(TEntity).GetProperties();

            // Detect PK property (except TenantId & CompanyId)
            var tenantProp = properties.FirstOrDefault(p => p.Name.Equals("TenantId", StringComparison.OrdinalIgnoreCase));
            var companyProp = properties.FirstOrDefault(p => p.Name.Equals("CompanyId", StringComparison.OrdinalIgnoreCase));
            var idProp = properties.FirstOrDefault(p =>
                Attribute.IsDefined(p, typeof(KeyAttribute)) ||
                p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) ||
                (p.Name.EndsWith("Id", StringComparison.OrdinalIgnoreCase) && p != tenantProp && p != companyProp));

            if (idProp == null)
                throw new Exception($"No primary key detected on {typeof(TEntity).Name}");

            // Find entity by PK + TenantId + CompanyId
            var existingEntity = await _dbSet.FirstOrDefaultAsync(e =>
                EF.Property<Guid>(e, tenantProp!.Name) == identity.TenantId &&
                EF.Property<Guid>(e, companyProp!.Name) == identity.CompanyId &&
                EF.Property<int>(e, idProp.Name) == id
            );

            if (existingEntity == null)
                throw new Exception("Entity not found");

            // Soft delete
            existingEntity.IsActive = false;
            existingEntity.UpdatedOn = DateTime.UtcNow;

            // Mark only audit fields as modified
            var entry = _context.Entry(existingEntity);
            entry.Property(nameof(existingEntity.IsActive)).IsModified = true;
            entry.Property(nameof(existingEntity.UpdatedOn)).IsModified = true;

            // Prevent accidental PK/tenant/company update
            entry.Property(tenantProp.Name).IsModified = false;
            entry.Property(companyProp.Name).IsModified = false;
            entry.Property(idProp.Name).IsModified = false;

            await _context.SaveChangesAsync();
        }

    //    public virtual async Task AddAsync(TEntity entity)
    //    {
    //        var identity = _identityService.GetIdentity();

    //        // 1️⃣ Tenant/Company set
    //        identity.TenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");
    //        identity.CompanyId = Guid.Parse("22222222-2222-2222-2222-222222222222");

    //        // 2️⃣ Detect key
    //        var entityType = typeof(TEntity);
    //        var properties = entityType.GetProperties();

    //        var tenantProp = properties.FirstOrDefault(p => p.Name.Equals("TenantId", StringComparison.OrdinalIgnoreCase));
    //        var companyProp = properties.FirstOrDefault(p => p.Name.Equals("CompanyId", StringComparison.OrdinalIgnoreCase));
    //        var idProp = properties.FirstOrDefault(p =>
    //            Attribute.IsDefined(p, typeof(KeyAttribute)) ||
    //            (p.Name.EndsWith("Id", StringComparison.OrdinalIgnoreCase)
    //             && p != tenantProp && p != companyProp));

    //        if (idProp == null)
    //            throw new Exception($"No primary key detected on {entityType.Name}");

    //        // 3️⃣ Generate new PK
    //        int newPk = await _identityService.GenerateNextPk<TEntity>(identity.TenantId, identity.CompanyId);
    //        idProp.SetValue(entity, Convert.ChangeType(newPk, idProp.PropertyType));

    //        // 4️⃣ Base identity fields
    //        SetIdentity(entity, identity);

    //        // 5️⃣ Add master first
    //        await _dbSet.AddAsync(entity);
    //        //await _context.SaveChangesAsync();

    //        // 6️⃣ Collect master PK for foreign key mapping
    //        var fkDict = new Dictionary<string, object>
    //{
    //    { idProp.Name, newPk }
    //};

    //        // 7️⃣ Handle all child entities recursively
    //        await AddChildEntitiesRecursive(entity, identity, fkDict);

    //        // 8️⃣ Save all children
    //        await _context.SaveChangesAsync();
    //    }

        //private async Task AddChildEntitiesRecursive(object parentEntity, IdentityModel identity, Dictionary<string, object>? parentKeys = null, HashSet<Type>? visited = null)
        //{
        //    visited ??= new HashSet<Type>();
        //    var entityType = parentEntity.GetType();

        //    if (visited.Contains(entityType))
        //        return;
        //    visited.Add(entityType);

        //    var properties = entityType.GetProperties();

        //    foreach (var prop in properties)
        //    {
        //        // 🔹 One-to-many child collection
        //        if (typeof(System.Collections.IEnumerable).IsAssignableFrom(prop.PropertyType)
        //            && prop.PropertyType != typeof(string))
        //        {
        //            var children = prop.GetValue(parentEntity) as System.Collections.IEnumerable;
        //            if (children == null) continue;

        //            foreach (var child in children)
        //            {
        //                // 1️⃣ Detect child's PK property
        //                var childType = child.GetType();
        //                var childProps = childType.GetProperties();
        //                var idProp = childProps.FirstOrDefault(p =>
        //                    Attribute.IsDefined(p, typeof(KeyAttribute)) ||
        //                    (p.Name.EndsWith("Id", StringComparison.OrdinalIgnoreCase) && !p.Name.Equals("TenantId") && !p.Name.Equals("CompanyId")));

        //                // 2️⃣ Set Tenant/Company/CreatedOn
        //                SetIdentity(child, identity, parentKeys);

        //                // 3️⃣ If parent has ID, match FK automatically
        //                if (parentKeys != null)
        //                {
        //                    foreach (var pk in parentKeys)
        //                    {
        //                        // উদাহরণ: যদি parent key "POId" হয়, তাহলে child-এ একই নাম থাকলে বসিয়ে দেবে
        //                        var fkProp = childProps.FirstOrDefault(p => p.Name.Equals(pk.Key, StringComparison.OrdinalIgnoreCase));
        //                        if (fkProp != null)
        //                            fkProp.SetValue(child, pk.Value);
        //                    }
        //                }

        //                // 4️⃣ Add child
        //                _context.Add(child);

        //                // 5️⃣ Recursively add child's own children (nested detail)
        //                var thisChildKeys = new Dictionary<string, object>();
        //                if (idProp != null)
        //                {
        //                    var pkValue = idProp.GetValue(child);
        //                    if (pkValue != null)
        //                        thisChildKeys[idProp.Name] = pkValue;
        //                }

        //                await AddChildEntitiesRecursive(child, identity, thisChildKeys, visited);
        //            }
        //        }
        //    }
        //}


        //private void SetIdentity(object entity, IdentityModel identity, Dictionary<string, object>? foreignKeys = null)
        //{
        //    if (entity == null) return;

        //    var props = entity.GetType().GetProperties();

        //    var tenantProp = props.FirstOrDefault(p => p.Name.Equals("TenantId", StringComparison.OrdinalIgnoreCase));
        //    var companyProp = props.FirstOrDefault(p => p.Name.Equals("CompanyId", StringComparison.OrdinalIgnoreCase));
        //    var isActiveProp = props.FirstOrDefault(p => p.Name.Equals("IsActive", StringComparison.OrdinalIgnoreCase));
        //    var createdOnProp = props.FirstOrDefault(p => p.Name.Equals("CreatedOn", StringComparison.OrdinalIgnoreCase));
        //    var modifiedOnProp = props.FirstOrDefault(p => p.Name.Equals("ModifiedOn", StringComparison.OrdinalIgnoreCase));

        //    tenantProp?.SetValue(entity, identity.TenantId);
        //    companyProp?.SetValue(entity, identity.CompanyId);
        //    isActiveProp?.SetValue(entity, true);

        //    if (createdOnProp != null && createdOnProp.GetValue(entity) == null)
        //        createdOnProp.SetValue(entity, DateTime.UtcNow);
        //    modifiedOnProp?.SetValue(entity, DateTime.UtcNow);

        //    // 🔹 Apply parent FK values if available
        //    if (foreignKeys != null)
        //    {
        //        foreach (var kvp in foreignKeys)
        //        {
        //            var fkProp = props.FirstOrDefault(p => p.Name.Equals(kvp.Key, StringComparison.OrdinalIgnoreCase));
        //            if (fkProp != null)
        //                fkProp.SetValue(entity, kvp.Value);
        //        }
        //    }
        //}

    }
}
