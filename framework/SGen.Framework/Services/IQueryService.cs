using Framework.Common;
using Framework.Entities;
using SGen.Framework.Common;

namespace Framework.Services
{
    public interface IQueryService<TEntity, TDto, TKey> where TEntity : BaseEntity<TKey>
    {
        public Task<PagedResult<IEnumerable<TDto>>> GetAllAsync(QueryParameters queryParams);
        public Task<IEnumerable<TDto>> GetAllAsync();
        public Task<TDto> GetByIdAsync(int id);
    }
}
