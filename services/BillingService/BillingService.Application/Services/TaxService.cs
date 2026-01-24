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


namespace BillingService.Application.Services;

public class TaxService : CrudService<Tax, TaxDto, long>, ITaxService
{
    private readonly SGenDbContext _context;
    private readonly IIdentityServices _identityService;
    private readonly IMapper _mapper;
    public TaxService(IQueryRepository<Tax, long> repository,
        IMapper mapper, SGenDbContext context, IIdentityServices identityService,
    ICommandRepository<Tax, long> commandRepo)
        : base(repository, mapper, commandRepo) // ✅ inject করে base এ পাঠাচ্ছি
    {
        _context = context;
        _identityService = identityService;
        _mapper = mapper;

    }

    public override async Task<TaxDto> CreateAsync(TaxDto objModel)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var identity = _identityService.GetIdentity();
            identity.TenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            identity.CompanyId = Guid.Parse("22222222-2222-2222-2222-222222222222");

            var getLedgerId = (await _context.LedgerMasters
                .Where(x => x.TenantId == identity.TenantId && x.CompanyId == identity.CompanyId)
                .Select(x => x.LedgerId)
                .ToListAsync())
                .DefaultIfEmpty(0)
                .Max() + 1;

            var subEntity = new LedgerMaster
            {
                TenantId = identity.TenantId,
                CompanyId = identity.CompanyId,
                LedgerId = getLedgerId,
                LedgerCateId = 2,
                ParentId = 14,
                LedgerName = objModel.Name,
                IsControlLedger = false,
                IsActive = true,
                CreatedBy = objModel.CreatedBy ?? "Admin",
                CreatedOn = DateTime.Now
            };

            await _context.LedgerMasters.AddAsync(subEntity);

            // Generate next Tax Master Id
            var getTaxId = (await _context.Taxes
                .Where(x => x.TenantId == identity.TenantId && x.CompanyId == identity.CompanyId)
                .Select(x => x.TaxId)
                .ToListAsync())
                .DefaultIfEmpty(0)
                .Max() + 1;

            // ====== 1. Tax master insert ======
            var entity = new Tax
            {
                TenantId = identity.TenantId,
                CompanyId = identity.CompanyId,
                TaxId = getTaxId,
                LedgerId = getLedgerId, 
                Name = objModel.Name,
                IsActive = true,
                CreatedBy = "Admin",
                CreatedOn = DateTime.Now
            };

            await _context.Taxes.AddAsync(entity);

            // ====== 1. New Ledger master insert ======

            await _context.SaveChangesAsync();  // Save to get 

            await transaction.CommitAsync();
            // ====== 3. Map back to DTO ======
            objModel.TaxId = entity.TaxId;
            return objModel;
        }
        catch (Exception ex)
        {

            await transaction.RollbackAsync();
            throw ex;
        }
    }
}

