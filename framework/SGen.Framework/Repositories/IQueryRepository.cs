using Framework.Common;
using Framework.Entities;
using SGen.Framework.Common;

namespace Framework.Repositories
{
    public interface IQueryRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
    {
        public Task<PagedResult<IEnumerable<TEntity>>> GetAllAsync(QueryParameters queryParams);
        public Task<IEnumerable<TEntity>> GetAllAsync();
        public Task<TEntity> GetByIdAsync(int id);
    }
}
