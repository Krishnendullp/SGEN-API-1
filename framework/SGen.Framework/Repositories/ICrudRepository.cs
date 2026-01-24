
using Framework.Entities;

namespace Framework.Repositories
{
    public interface ICrudRepository<TEntity, TKey> :
        IQueryRepository<TEntity, TKey>,
        ICommandRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
    {
    }
}
