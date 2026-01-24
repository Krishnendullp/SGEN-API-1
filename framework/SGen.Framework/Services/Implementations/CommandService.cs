using Framework.Entities;
using Framework.Repositories;

namespace Framework.Services.Implementations
{
    public class CommandService<TEntity,TDto, TKey> : ICommandService<TEntity,TDto, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly ICommandRepository<TEntity, TKey> _repository;

        public CommandService(ICommandRepository<TEntity, TKey> repository)
        {
            _repository = repository;
        }

        public  async Task<TDto> CreateAsync(TDto entity)
        {
            throw new NotImplementedException();
        }

        public async Task<TDto> UpdateAsync(TDto entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
