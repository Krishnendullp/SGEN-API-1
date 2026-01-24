using BillingService.Application.Interfaces;
using BillingService.Domain.Entities;
using Framework.Entities;
using Framework.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGen.Framework.Entities;

namespace BillingService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubLedgerController : BaseCrudController<SubLedger, SubLedgerDto, long>
    {
        public SubLedgerController(ISubLedgerService service) : base(service)
        {
        }
    }
}
