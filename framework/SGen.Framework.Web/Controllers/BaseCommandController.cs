using Framework.Entities;
using Framework.Services;
using Microsoft.AspNetCore.Mvc;

namespace Framework.Web.Controllers
{
    public abstract class BaseCommandController<TEntity,TDto, TKey> : BaseController where TEntity : BaseEntity<TKey>
    {
        private readonly ICommandService<TEntity,TDto, TKey> _commandService;

        protected BaseCommandController(ICommandService<TEntity,TDto, TKey> commandService)
        {
            _commandService = commandService;
        }

        [HttpPost("Add")]
        public virtual async Task<IActionResult> Create([FromBody] TDto entity)
        {
            var created = await _commandService.CreateAsync(entity);
            return Success(created, "Created successfully");
        }

        [HttpPut("Update")]
        public virtual async Task<IActionResult> Update([FromBody] TDto entity)
        {
            var updated = await _commandService.UpdateAsync(entity);
            return Success(updated, "Updated successfully");
        }

        [HttpDelete("Delete")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var result = await _commandService.DeleteAsync(id);
            return result ? Success(id, "Deleted successfully") : Failure("Delete failed");
        }
    }
}
