using AutoMapper;
using BillingService.Application.Interfaces;
using BillingService.Domain.Entities;
using Framework.Entities;
using Framework.Repositories;
using Framework.Services.Implementations;
using SGen.Framework.Contexts;
using SGen.Framework.Entities;
using SGen.Framework.Services;


namespace BillingService.Application.Services
{
    public class SaleItemMasterService : CrudService<SaleItemMaster, SaleItemMasterDto, long>, ISaleItemMasterSecvice
    {
        private readonly SGenDbContext _context;
        private readonly IIdentityServices _identityService;
        private readonly IMapper _mapper;
        public SaleItemMasterService(IQueryRepository<SaleItemMaster, long> repository,
            IMapper mapper, SGenDbContext context, IIdentityServices identityService,
        ICommandRepository<SaleItemMaster, long> commandRepo)
            : base(repository, mapper, commandRepo) // ✅ inject করে base এ পাঠাচ্ছি
        {
            _context = context;
            _identityService = identityService;
            _mapper = mapper;

        }
    }
}
