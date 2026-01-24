using System;
using AutoMapper;
using BillingService.Application.Interfaces;
using BillingService.Domain.Entities;
using Framework.Repositories;
using Framework.Services.Implementations;
using SGen.Framework.Contexts;
using SGen.Framework.Entities;
using SGen.Framework.Services;

namespace BillingService.Application.Services
{
    public class LedgerCategoryService : CrudService<LedgerCategory, LedgerCategoryDto, long>, ILedgerCategoryService
    {
        private readonly SGenDbContext _context;
        private readonly IIdentityServices _identityService;
        private readonly IMapper _mapper;
        public LedgerCategoryService(IQueryRepository<LedgerCategory, long> repository,
            IMapper mapper, SGenDbContext context, IIdentityServices identityService,
        ICommandRepository<LedgerCategory, long> commandRepo)
            : base(repository, mapper, commandRepo) // ✅ inject করে base এ পাঠাচ্ছি
        {
            _context = context;
            _identityService = identityService;
            _mapper = mapper;

        }
    }
}
