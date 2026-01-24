using Framework.Entities;

namespace Framework.Services
{
    public interface ICrudService<TEntity, TDto, TKey> : IQueryService<TEntity, TDto, TKey>, ICommandService<TEntity,TDto, TKey> where TEntity : BaseEntity<TKey>
    {
    }
}
