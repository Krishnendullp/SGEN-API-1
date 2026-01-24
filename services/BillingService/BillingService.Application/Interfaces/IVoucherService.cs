using System;
using BillingService.Domain.Entities;
using Framework.Services;
using SGen.Framework.Entities;

namespace BillingService.Application.Interfaces
{
    public interface IVoucherService : ICrudService<VoucherMaster, VoucherMasterDto, long>
    {
    }
}
