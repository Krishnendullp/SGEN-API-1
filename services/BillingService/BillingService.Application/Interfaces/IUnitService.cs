using BillingService.Domain.Entities;
using Framework.Entities;
using Framework.Services;

namespace BillingService.Application.Interfaces;

public interface IUnitService : ICrudService<Unit, UnitDto, long>
{
}
