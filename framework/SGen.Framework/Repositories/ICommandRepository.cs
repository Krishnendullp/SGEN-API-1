
using Framework.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Framework.Repositories
{
    public interface ICommandRepository<TEntity,TKey>
        where TEntity : BaseEntity<TKey>
    {
        public Task AddAsync(TEntity entity);
        public Task UpdateAsync(TEntity entity);
        public Task DeleteAsync(int id);
    }
}
