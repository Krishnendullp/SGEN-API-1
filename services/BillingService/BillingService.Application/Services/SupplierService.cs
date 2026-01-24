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
using SGen.Framework.Services.Implementations;


namespace BillingService.Application.Services
{
    public class SupplierService : CrudService<SupplierMaster, SupplierMasterDto, long>, ISupplierService
    {
        private readonly SGenDbContext _context;
        private readonly IIdentityServices _identityService;
        private readonly IMapper _mapper;
        public SupplierService(IQueryRepository<SupplierMaster, long> repository, 
            IMapper mapper, SGenDbContext context, IIdentityServices identityService,
            ICommandRepository<SupplierMaster, long> commandRepo)
        : base(repository, mapper, commandRepo)
        {
            _context = context;
            _identityService = identityService;
            _mapper = mapper;
        }
        public override async Task<SupplierMasterDto> CreateAsync(SupplierMasterDto objModel)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var identity = _identityService.GetIdentity();
                identity.TenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");
                identity.CompanyId = Guid.Parse("22222222-2222-2222-2222-222222222222");

                // ====== 1. New Ledger master insert ======

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
                    ParentId = objModel.LedgerMasters?.LedgerId ?? 4,
                    LedgerName = objModel.SupplierName,
                    IsControlLedger = false,
                    Mobile = objModel.Phone,
                    Address = objModel.Address,
                    GstNo = objModel.GstNumber,
                    IsActive = true,
                    CreatedBy = objModel.CreatedBy ?? "Admin",
                    CreatedOn = DateTime.Now
                };

                await _context.LedgerMasters.AddAsync(subEntity);

                // Generate next Supplier  Id

                var getSupplierId = (await _context.SupplierMasters
                    .Where(x => x.TenantId == identity.TenantId && x.CompanyId == identity.CompanyId)
                    .Select(x => x.SupplierId)
                    .ToListAsync())
                    .DefaultIfEmpty(0)
                    .Max() + 1;

                // ====== 1. Supplier Return master insert ======
                var entity = new SupplierMaster
                {
                    TenantId = identity.TenantId,
                    CompanyId = identity.CompanyId,
                    SupplierId = getSupplierId,
                    LedgerId = getLedgerId,
                    SupplierCode = $"SUP-00{getSupplierId}",
                    SupplierName = objModel.SupplierName,
                    ContactPerson = objModel.ContactPerson,
                    Phone = objModel.Phone,
                    Email = objModel.Email,
                    Address = objModel.Address,
                    City = objModel.City,
                    State = objModel.State,
                    Pincode = objModel.Pincode,
                    Country = objModel.Country,
                    GstNumber = objModel.GstNumber,
                    IsActive = true,
                    CreatedBy = "Admin",
                    CreatedOn = DateTime.Now
                };

                await _context.SupplierMasters.AddAsync(entity);
                await _context.SaveChangesAsync();  // Save to get

                await transaction.CommitAsync();

                // ====== 3. Map back to DTO ======
                objModel.SupplierId = entity.SupplierId;
                return objModel;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw ex;
            }
        }

    }
}
