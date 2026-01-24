using BillingService.Domain.Entities;
using Framework.Entities;
using Framework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingService.Application.Interfaces
{
    public interface ITaxGroupIgstDetailService : ICrudService<TaxGroupIgstDetail, TaxGroupIgstDetailDto, long>
    {
    }
}
