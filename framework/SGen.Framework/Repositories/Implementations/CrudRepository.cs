using Framework.Common;
using Framework.Entities;
using Microsoft.EntityFrameworkCore;
using SGen.Framework.Common;
using SGen.Framework.Contexts;
using SGen.Framework.Helper;
using SGen.Framework.Services;
using System.Security.Principal;

namespace Framework.Repositories.Implementations
{
    public class CrudRepository<TEntity, TKey> : BaseRepository<TEntity, TKey>, IQueryRepository<TEntity, TKey>, 
        ICommandRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public CrudRepository(SGenDbContext context, IIdentityServices identityService)
        : base(context, identityService) { }

        public async Task AddAsync(TEntity entity)
        {
            var identity = _identityService.GetIdentity();
            entity.TenantId = identity.TenantId;
            entity.CompanyId = identity.CompanyId;

            // 3️⃣ Detect primary key property dynamically
            var pkProperty = typeof(TEntity).GetProperties()
                .FirstOrDefault(p =>
                    Attribute.IsDefined(p, typeof(System.ComponentModel.DataAnnotations.KeyAttribute)) ||
                    p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) ||
                    p.Name.EndsWith("Id", StringComparison.OrdinalIgnoreCase));

            if (pkProperty == null)
                throw new Exception($"No primary key detected on {typeof(TEntity).Name}");

            // 4️⃣ Generate next PK
            var newPk = await _identityService.GenerateNextPk<TEntity>(identity.TenantId, identity.CompanyId);

            // 5️⃣ Set PK safely
            pkProperty.SetValue(entity, Convert.ChangeType(newPk, pkProperty.PropertyType));

            // 6️⃣ Add entity to DbSet
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var identity = _identityService.GetIdentity();

            var pkProperty = typeof(TEntity).GetProperties()
                .FirstOrDefault(p =>
                    Attribute.IsDefined(p, typeof(System.ComponentModel.DataAnnotations.KeyAttribute)) ||
                    p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) ||
                    p.Name.EndsWith("Id", StringComparison.OrdinalIgnoreCase));

            if (pkProperty == null)
                throw new Exception($"No primary key detected on {typeof(TEntity).Name}");

            var pkValue = pkProperty.GetValue(id);
            if (pkValue == null)
                throw new Exception("Primary key value is null");

            var existingEntity = await _dbSet
                .Where(e => EF.Property<Guid>(e, "TenantId") == identity.TenantId &&
                            EF.Property<Guid>(e, "CompanyId") == identity.CompanyId &&
                            EF.Property<int>(e, pkProperty.Name) == (int)pkValue)
                .FirstOrDefaultAsync();

            if (existingEntity == null)
                throw new Exception("Entity not found for delete");

            existingEntity.IsActive = false;
            existingEntity.UpdatedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
        public async Task<PagedResult<IEnumerable<TEntity>>> GetAllAsync(QueryParameters queryParams)
        {
            try
            {
                var query = _context.Set<TEntity>().AsQueryable();

                query = query.ApplyFilters(queryParams.Filters)
                             .ApplySorting(queryParams.Sorts);

                var list = await query.ToListAsync();

                if (queryParams.PageNo <= 0 || queryParams.PageSize <= 0)
                {
                    var allItems = await query.ToListAsync();
                    return new PagedResult<IEnumerable<TEntity>>
                    {
                        Data = allItems,
                        PageNo = 1,
                        PageSize = allItems.Count,
                        TotalCount = allItems.Count
                    };
                }
                return await query.ToPagedResultAsync(queryParams.PageNo, queryParams.PageSize);
            }
            catch (Exception ex)
            {
                return PagedResult<IEnumerable<TEntity>>.Fail($"Error: {ex.Message}");
                
            }
        }

        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> Query()
        {
            var identity = _identityService.GetIdentity();
            return _dbSet.Where(e => e.TenantId == identity.TenantId &&
                                     e.CompanyId == identity.CompanyId &&
                                     !e.IsActive);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            var identity = _identityService.GetIdentity();

            var pkProperty = typeof(TEntity).GetProperties()
                .FirstOrDefault(p =>
                    Attribute.IsDefined(p, typeof(System.ComponentModel.DataAnnotations.KeyAttribute)) ||
                    p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) ||
                    p.Name.EndsWith("Id", StringComparison.OrdinalIgnoreCase));

            if (pkProperty == null)
                throw new Exception($"No primary key detected on {typeof(TEntity).Name}");

            var pkValue = pkProperty.GetValue(entity);
            if (pkValue == null)
                throw new Exception("Primary key value is null");

            // Fetch existing entity by TenantId + CompanyId + PK
            var existingEntity = await _dbSet
                .Where(e => EF.Property<Guid>(e, "TenantId") == identity.TenantId &&
                            EF.Property<Guid>(e, "CompanyId") == identity.CompanyId &&
                            EF.Property<int>(e, pkProperty.Name) == (int)pkValue)
                .FirstOrDefaultAsync();

            if (existingEntity == null)
                throw new Exception("Entity not found for update");

            // Copy all properties except TenantId, CompanyId, PK
            foreach (var prop in typeof(TEntity).GetProperties())
            {
                if (prop.Name == pkProperty.Name || prop.Name == "TenantId" || prop.Name == "CompanyId")
                    continue;

                var newValue = prop.GetValue(entity);
                prop.SetValue(existingEntity, newValue);
            }

            existingEntity.UpdatedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }
}
