using BillingService.Application.Interfaces;
using BillingService.Domain.Entities;
using Framework.Entities;
using Framework.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BillingService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemMasterController : BaseCrudController<ItemMaster, ItemMasterDto, long>
{
    public ItemMasterController(IItemService service) : base(service)
    {
    }
}
