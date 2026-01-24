using AutoMapper;
using BillingService.Application.Interfaces;
using BillingService.Domain.Entities;
using Framework.Entities;
using Framework.Repositories;
using Framework.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingService.Application.Services
{
    public class TaxGroupIgstDetailService : CrudService<TaxGroupIgstDetail, TaxGroupIgstDetailDto, long>, ITaxGroupIgstDetailService
    {
        public TaxGroupIgstDetailService(IQueryRepository<TaxGroupIgstDetail, long> repository, IMapper mapper, ICommandRepository<TaxGroupIgstDetail, long> commandRepo)
            : base(repository, mapper, commandRepo)
        {
        }
    }
}
