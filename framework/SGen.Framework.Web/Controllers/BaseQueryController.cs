using Framework.Entities;
using Framework.Services;
using Microsoft.AspNetCore.Mvc;
using SGen.Framework.Common;

namespace Framework.Web.Controllers
{
    public abstract class BaseQueryController<TEntity,TDto, TKey> : BaseController where TEntity : BaseEntity<TKey>
    {
        private readonly IQueryService<TEntity,TDto, TKey> _queryService;

        protected BaseQueryController(IQueryService<TEntity,TDto, TKey> queryService)
        {
            _queryService = queryService;
        }

        [HttpPost("Search")]
        public async Task<PagedResult<IEnumerable<TDto>>> GetAllAsync([FromBody] QueryParameters queryParams)
        {
            
            var resultFromRepo = await _queryService.GetAllAsync(queryParams); // Result<IEnumerable<TEntity>>

            if (!resultFromRepo.Success || resultFromRepo.Data == null || !resultFromRepo.Data.Any())
                return PagedResult<IEnumerable<TDto>>.Fail("No records found.");

            return PagedResult<IEnumerable<TDto>>.Ok(resultFromRepo.Data, "Records fetched successfully.",
                                                    resultFromRepo.PageNo,resultFromRepo.PageSize,resultFromRepo.TotalCount);
        }

        [HttpGet("GetAll")]
        public virtual async Task<IActionResult> GetAll()
        {
            try
            {
                var entity = await _queryService.GetAllAsync();
                return entity != null ? Success(entity) : Failure("Not found");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpGet("ById")]
        public virtual async Task<IActionResult> GetById(int id)
        {
            var entity = await _queryService.GetByIdAsync(id);
            return entity != null ? Success(entity) : Failure("Not found");
        }

    }
}
