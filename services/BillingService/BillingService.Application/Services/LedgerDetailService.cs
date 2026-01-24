using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BillingService.Application.Interfaces;
using BillingService.Domain.Entities;
using Framework.Entities;
using Framework.Repositories;
using Framework.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using SGen.Framework.Contexts;
using SGen.Framework.Entities;
using SGen.Framework.Services;

namespace BillingService.Application.Services
{
    public class LedgerDetailService : CrudService<LedgerDetail, LedgerDetailDto, long>, ILedgerDetailService
    {
        private readonly SGenDbContext _context;
        private readonly IIdentityServices _identityService;
        private readonly IMapper _mapper;
        public LedgerDetailService(IQueryRepository<LedgerDetail, long> repository,
            IMapper mapper, SGenDbContext context, IIdentityServices identityService,
        ICommandRepository<LedgerDetail, long> commandRepo)
            : base(repository, mapper, commandRepo) // ✅ inject করে base এ পাঠাচ্ছি
        {
            _context = context;
            _identityService = identityService;
            _mapper = mapper;

        }

        public override async Task<IEnumerable<LedgerDetailDto>> GetAllAsync()
        {
            try
            {
                var identity = _identityService.GetIdentity();

                // For testing/demo
                identity.TenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");
                identity.CompanyId = Guid.Parse("22222222-2222-2222-2222-222222222222");

                // ✅ Include related tables dynamically
                var query = _context.Set<LedgerDetail>()    // Example 1
                    .Include(x => x.Ledger)
                    .Include(x => x.SubLedger)
                    .Where(x =>
                        x.TenantId == identity.TenantId &&
                        x.CompanyId == identity.CompanyId &&
                        x.IsActive == true);

                //var entity = await query.FirstOrDefaultAsync();

                if (query == null)
                    throw new Exception($"Purchase Order not found ");

                // ✅ Map to DTO before returning
                var dto = _mapper.Map<IEnumerable<LedgerDetailDto>>(query);
                return dto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public override async Task<LedgerDetailDto> GetByIdAsync(int id)
        {
            try
            {
                var identity = _identityService.GetIdentity();

                // For testing/demo
                identity.TenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");
                identity.CompanyId = Guid.Parse("22222222-2222-2222-2222-222222222222");

                // ✅ Include related tables dynamically
                var query = _context.Set<LedgerDetail>()    
                    .Include(x => x.Ledger)
                    .Include(x => x.SubLedger)
                    .Where(x =>
                        x.TenantId == identity.TenantId &&
                        x.CompanyId == identity.CompanyId &&
                        x.IsActive == true &&
                        x.Id == id);

                var entity = await query.FirstOrDefaultAsync();

                if (entity == null)
                    throw new Exception($"Purchase Order not found with Id={id}");

                // ✅ Map to DTO before returning
                var dto = _mapper.Map<LedgerDetailDto>(entity);
                return dto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
