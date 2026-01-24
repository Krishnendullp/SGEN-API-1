using Framework.Entities;
using Framework.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.Web.Controllers
{
    public abstract class BaseCrudController<TEntity, TDto, TKey> :
        BaseQueryController<TEntity,TDto, TKey>,
        ICommandService<TEntity,TDto, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly ICrudService<TEntity, TDto, TKey> _crudService;

        protected BaseCrudController(ICrudService<TEntity, TDto, TKey> crudService)
            : base(crudService)
        {
            _crudService = crudService;
        }

        [HttpPost("Add")]
        public Task<TDto> CreateAsync([FromBody] TDto entity) 
            => _crudService.CreateAsync(entity);

        [HttpDelete("Delete")]
        public Task<bool> DeleteAsync(int id) 
            => _crudService.DeleteAsync(id);

        //[HttpGet("by-id/{id}")]
        //public Task<TDto> GetByIdAsync(int id)
        //    => _crudService.GetByIdAsync(id);

        [HttpPut("Update")]
        public Task<TDto> UpdateAsync([FromBody] TDto entity) 
            => _crudService.UpdateAsync(entity);
    }
}
