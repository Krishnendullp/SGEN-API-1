using BillingService.Application.Interfaces;
using BillingService.Domain.Entities;
using Framework.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using SGen.Framework.Entities;

namespace BillingService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleMasterController : BaseCrudController<SaleMaster, SaleMasterDto, long>
    {
        public SaleMasterController(ISaleMasterService service) : base(service)
        {
        }
    }
}
