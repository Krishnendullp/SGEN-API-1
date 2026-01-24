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
    public class CustomerService : CrudService<CustomerMaster, CustomerMasterDto, long>, ICustomerService
    {
        private readonly SGenDbContext _context;
        private readonly IIdentityServices _identityService;
        private readonly IMapper _mapper;
        public CustomerService(IQueryRepository<CustomerMaster, long> repository,
            IMapper mapper, SGenDbContext context, IIdentityServices identityService,
        ICommandRepository<CustomerMaster, long> commandRepo)
            : base(repository, mapper, commandRepo) // ✅ inject করে base এ পাঠাচ্ছি
        {
            _context = context;
            _identityService = identityService;
            _mapper = mapper;

        }

        public override async Task<CustomerMasterDto> CreateAsync(CustomerMasterDto objModel)
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
                    LedgerCateId = 1,
                    ParentId = objModel.LedgerMasters?.LedgerId ?? 5,
                    LedgerName = objModel.CustomerName,
                    IsControlLedger = false,
                    Mobile = objModel.Phone,
                    Address = objModel.Address,
                    GstNo = objModel.GstNumber,
                    IsActive = true,
                    CreatedBy = "Admin",
                    CreatedOn = DateTime.Now
                };
                await _context.LedgerMasters.AddAsync(subEntity);

                // Generate next Customer Master Id

                var getCustomerId = (await _context.CustomerMasters
                    .Where(x => x.TenantId == identity.TenantId && x.CompanyId == identity.CompanyId)
                    .Select(x => x.CustomerId)
                    .ToListAsync())
                    .DefaultIfEmpty(0)
                    .Max() + 1;

                // ====== 1. Customer master insert ======
                var entity = new CustomerMaster
                {
                    TenantId = identity.TenantId,
                    CompanyId = identity.CompanyId,
                    CustomerId = getCustomerId,
                    LedgerId = getLedgerId,
                    CustomerCode = $"CUST-00{getCustomerId}",
                    CustomerName = objModel.CustomerName,
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
                    CreatedBy = objModel.CreatedBy ?? "Admin",
                    CreatedOn = DateTime.Now
                };

                await _context.CustomerMasters.AddAsync(entity);   
                await _context.SaveChangesAsync();  // Save to get 

                await transaction.CommitAsync();
                // ====== 3. Map back to DTO ======
                objModel.CustomerId = entity.CustomerId;
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
