using Framework.Common;
using Framework.Entities;
using Microsoft.EntityFrameworkCore;
using SGen.Framework.Common;
using SGen.Framework.Contexts;
using SGen.Framework.Helper;
using SGen.Framework.Services;
using System.ComponentModel.DataAnnotations;

namespace Framework.Repositories.Implementations
{
    public class QueryRepository <TEntity, TKey> : BaseRepository<TEntity, TKey>, 
        IQueryRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
    {
        public QueryRepository(SGenDbContext context, IIdentityServices identityService)
        : base(context, identityService) { }

        public async Task<PagedResult<IEnumerable<TEntity>>> GetAllAsync(QueryParameters queryParams)
        {
            try
            {
                var identity = _identityService.GetIdentity();

                identity.TenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");
                identity.CompanyId = Guid.Parse("22222222-2222-2222-2222-222222222222");

                var query = _context.Set<TEntity>().AsQueryable().Where(x => x.TenantId == identity.TenantId 
                                                    && x.CompanyId == identity.CompanyId && x.IsActive == true);

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

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            try
            {
                var identity = _identityService.GetIdentity();

                identity.TenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");
                identity.CompanyId = Guid.Parse("22222222-2222-2222-2222-222222222222");

                var query = _context.Set<TEntity>().AsQueryable().Where(x => x.TenantId == identity.TenantId
                                                    && x.CompanyId == identity.CompanyId && x.IsActive == true);

                //query = query.ApplyFilters(queryParams.Filters)
                //             .ApplySorting(queryParams.Sorts);

                //var list = await query.ToListAsync();

                return query;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GetAllAsync] Error in {typeof(TEntity).Name}: {ex.Message}");
                return null;
            }
        }

        public virtual async Task<TEntity?> GetByIdAsync(int id)
        {
            try
            {
                var identity = _identityService.GetIdentity();

                // ✅ For testing/demo purpose
                identity.TenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");
                identity.CompanyId = Guid.Parse("22222222-2222-2222-2222-222222222222");

                var properties = typeof(TEntity).GetProperties();

                // ✅ Detect property names dynamically
                var tenantProp = properties.FirstOrDefault(p => p.Name.Equals("TenantId", StringComparison.OrdinalIgnoreCase));
                var companyProp = properties.FirstOrDefault(p => p.Name.Equals("CompanyId", StringComparison.OrdinalIgnoreCase));
                var isActiveProp = properties.FirstOrDefault(p => p.Name.Equals("IsActive", StringComparison.OrdinalIgnoreCase));
                var idProp = properties.FirstOrDefault(p =>
                    Attribute.IsDefined(p, typeof(KeyAttribute)) ||
                    p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) ||
                    (p.Name.EndsWith("Id", StringComparison.OrdinalIgnoreCase)
                     && p != tenantProp && p != companyProp));

                if (idProp == null)
                    throw new Exception($"No primary key detected on {typeof(TEntity).Name}");

                // ✅ Query entity with filters
                var entity = await _dbSet.FirstOrDefaultAsync(e =>
                    EF.Property<Guid>(e, tenantProp!.Name) == identity.TenantId &&
                    EF.Property<Guid>(e, companyProp!.Name) == identity.CompanyId &&
                    EF.Property<bool>(e, isActiveProp!.Name) == true &&
                    EF.Property<int>(e, idProp.Name) == id);

                if (entity == null)
                    throw new Exception($"{typeof(TEntity).Name} not found with Id={id}");

                return entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GetByIdAsync] Error in {typeof(TEntity).Name}: {ex.Message}");
                return null;
            }
        }
        public IQueryable<TEntity> Query()
        {
            throw new NotImplementedException();
        }
    }
}
