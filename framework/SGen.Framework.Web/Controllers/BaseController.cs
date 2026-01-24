using Framework.Common;
using Microsoft.AspNetCore.Mvc;

namespace Framework.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult Success<T>(T data, string? message = null)
        {
            return Ok(Result<T>.Ok(data, message));
        }

        protected IActionResult Failure(string message)
        {
            return BadRequest(Result<string>.Fail(message));
        }
    }
}
