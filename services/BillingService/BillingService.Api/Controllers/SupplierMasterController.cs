using BillingService.Application.Interfaces;
using BillingService.Domain.Entities;
using Framework.Entities;
using Framework.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BillingService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SupplierMasterController : BaseCrudController<SupplierMaster, SupplierMasterDto, long>
{
    public SupplierMasterController(ISupplierService service) : base(service)
    {
    }
}
