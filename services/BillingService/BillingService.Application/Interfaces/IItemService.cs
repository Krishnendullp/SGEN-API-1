using BillingService.Domain.Entities;
using Framework.Entities;
using Framework.Services;

namespace BillingService.Application.Interfaces;

public interface IItemService : ICrudService<ItemMaster, ItemMasterDto, long>
{
}
