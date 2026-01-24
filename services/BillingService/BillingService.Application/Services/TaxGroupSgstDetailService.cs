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
    public class TaxGroupSgstDetailService : CrudService<TaxGroupSgstDetail, TaxGroupSgstDetailDto, long>, ITaxGroupSgstDetailService
    {
        public TaxGroupSgstDetailService(IQueryRepository<TaxGroupSgstDetail, long> repository, IMapper mapper, ICommandRepository<TaxGroupSgstDetail, long> commandRepo)
            : base(repository, mapper, commandRepo)
        {
        }
    }
}