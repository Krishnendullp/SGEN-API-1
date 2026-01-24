using AutoMapper;
using Framework.Common;
using Framework.Entities;
using Framework.Repositories;
using SGen.Framework.Common;

namespace Framework.Services.Implementations
{
    public class CrudService<TEntity, TDto, TKey> : ICrudService<TEntity, TDto, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly IQueryRepository<TEntity, TKey> _repository;
        protected readonly ICommandRepository<TEntity, TKey> _commandRepo;
        private readonly IMapper _mapper;

        // ✅ Constructor injection
        public CrudService(IQueryRepository<TEntity, TKey> repository, IMapper mapper, ICommandRepository<TEntity, TKey> commandRepo)
        {
            _repository = repository;
            _mapper = mapper;
            _commandRepo = commandRepo;
        }
        public virtual async Task<TDto> CreateAsync(TDto entity)
        {
            // dto → entity
            var rep = _mapper.Map<TEntity>(entity);

            // repo update (void return kore)
            await _commandRepo.AddAsync(rep);

            // repo return nei → tai just dto ke return kor
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _commandRepo.DeleteAsync(id);
            return true;
        }

        public virtual async Task<PagedResult<IEnumerable<TDto>>> GetAllAsync(QueryParameters queryParams)
        {
            // repository থেকে call
            var result = await _repository.GetAllAsync(queryParams);
            var dtos = _mapper.Map<IEnumerable<TDto>>(result.Data);
            return PagedResult<IEnumerable<TDto>>.Ok(dtos, result.Message,
                                result.PageNo, result.PageSize, result.TotalCount);
        }

        public virtual async Task<TDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return default;

            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task<TDto> UpdateAsync(TDto dto)
        {
            // dto → entity
            var entity = _mapper.Map<TEntity>(dto);

            // repo update (void return kore)
            await _commandRepo.UpdateAsync(entity);

            // repo return nei → tai just dto ke return kor
            return dto;
        }

        public virtual async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var result = await _repository.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<TDto>>(result);
            return dtos;
        }
    }
}
