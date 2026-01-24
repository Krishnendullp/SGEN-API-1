using BillingService.Domain.Entities;
using Framework.Services;
using SGen.Framework.Entities;
using System;


namespace BillingService.Application.Interfaces
{
    public interface ISaleMasterService : ICrudService<SaleMaster, SaleMasterDto, long>
    {
    }
}
