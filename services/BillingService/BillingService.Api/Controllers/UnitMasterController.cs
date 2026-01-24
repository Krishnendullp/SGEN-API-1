using BillingService.Application.Interfaces;
using BillingService.Domain.Entities;
using Framework.Entities;
using Framework.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillingService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UnitMasterController : BaseCrudController<Unit, UnitDto, long>
{
    public UnitMasterController(IUnitService service) : base(service)
    {
    }
}