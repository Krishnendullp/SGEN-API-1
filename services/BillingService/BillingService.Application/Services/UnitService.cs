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

namespace BillingService.Application.Services;
public class UnitService : CrudService<Unit, UnitDto, long>, IUnitService
{
    public UnitService(IQueryRepository<Unit, long> repository, IMapper mapper, ICommandRepository<Unit, long> commandRepo)
        : base(repository, mapper, commandRepo)
    {
    }
}
