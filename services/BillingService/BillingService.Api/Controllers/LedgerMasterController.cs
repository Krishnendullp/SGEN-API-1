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
    public class LedgerMasterController : BaseCrudController<LedgerMaster, LedgerMasterDto, long>
    {
        public LedgerMasterController(ILedgerMasterService service) : base(service)
        {
        }
    }
}
