
using Framework.Entities;

namespace Framework.Services
{
    public interface ICommandService<TEntity ,TDto, TKey> where TEntity : BaseEntity<TKey>
    {
      public Task<TDto> CreateAsync(TDto entity);
      public Task<TDto> UpdateAsync(TDto entity);
      public Task<bool> DeleteAsync(int id);
    }
}
