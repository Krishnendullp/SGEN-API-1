using BillingService.Application.Interfaces;
using BillingService.Domain.Entities;
using Framework.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGen.Framework.Entities;

namespace BillingService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LedgerCategoryController : BaseCrudController<LedgerCategory, LedgerCategoryDto, long>
    {
        public LedgerCategoryController(ILedgerCategoryService service) : base(service)
        {
        }
    }
}
