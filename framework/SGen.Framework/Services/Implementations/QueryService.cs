using AutoMapper;
using Framework.Common;
using Framework.Entities;
using Framework.Repositories;
using SGen.Framework.Common;

namespace Framework.Services.Implementations
{
    public class QueryService<TEntity, TDto,  TKey> : IQueryService<TEntity, TDto, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly IQueryRepository<TEntity, TKey> _repository;
        private readonly IMapper _mapper;
        public QueryService(IQueryRepository<TEntity, TKey> repository , IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<PagedResult<IEnumerable<TDto>>> GetAllAsync(QueryParameters queryParams)
        {
            // repository থেকে call
            var result = await _repository.GetAllAsync(queryParams);
            var dtos = _mapper.Map<IEnumerable<TDto>>(result.Data);
            return PagedResult<IEnumerable<TDto>>.Ok(dtos, result.Message,
            result.PageNo, result.PageSize, result.TotalCount);
        }

        public virtual async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var result = await _repository.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<TDto>>(result);
            return dtos;

        }

        public virtual async Task<TDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return default;

            return _mapper.Map<TDto>(entity);
        }
    }
}
