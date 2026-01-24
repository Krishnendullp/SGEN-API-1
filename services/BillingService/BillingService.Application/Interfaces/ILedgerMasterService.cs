using System;
using System.Collections.Generic;
using BillingService.Domain.Entities;
using Framework.Services;
using SGen.Framework.Entities;

namespace BillingService.Application.Interfaces
{
    public interface ILedgerMasterService : ICrudService<LedgerMaster, LedgerMasterDto, long>
    {
    }
}
