using BillingService.Application.Interfaces;
using BillingService.Application.Services;
using BillingService.Domain.Entities;
using Framework.Entities;
using Framework.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillingService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PurchaseController : BaseCrudController<PurchaseOrder, PurchaseOrderDto, long>
{
    public PurchaseController(IPurchaseService service) : base(service)
    {
    }
}   
