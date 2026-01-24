using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillingService.Domain.Entities;
using Framework.Services;
using SGen.Framework.Entities;

namespace BillingService.Application.Interfaces
{
    public interface ILedgerCategoryService : ICrudService<LedgerCategory, LedgerCategoryDto, long>
    {
    }
}
