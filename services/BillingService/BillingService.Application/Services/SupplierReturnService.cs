using AutoMapper;
using BillingService.Application.Interfaces;
using BillingService.Domain.Entities;
using Framework.Entities;
using Framework.Repositories;
using Framework.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using SGen.Framework.Contexts;
using SGen.Framework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingService.Application.Services
{
    public class SupplierReturnService : CrudService<SupplierReturn, SupplierReturnDto, long>, ISupplierReturnService
    {
        private readonly SGenDbContext _context;
        private readonly IMapper _mapper;
        private readonly IIdentityServices _identityService;
        public SupplierReturnService(IQueryRepository<SupplierReturn, long> repository,
            SGenDbContext context,
            IMapper mapper,
            IIdentityServices identityService,
            ICommandRepository<SupplierReturn, long> commandRepo)
            : base(repository, mapper, commandRepo)
        {
            _context = context;
            _identityService = identityService;
            _mapper = mapper;
        }

        public override async Task<SupplierReturnDto> CreateAsync(SupplierReturnDto objModel)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var identity = _identityService.GetIdentity();
                identity.TenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");
                identity.CompanyId = Guid.Parse("22222222-2222-2222-2222-222222222222");

                // Generate next Supplier Return Id
                var getReturnId = (await _context.SupplierReturns
                    .Where(x => x.TenantId == identity.TenantId && x.CompanyId == identity.CompanyId)
                    .Select(x => x.ReturnId)
                    .ToListAsync())
                    .DefaultIfEmpty(0)
                    .Max() + 1;

                // ====== 1. Supplier Return master insert ======
                var entity = new SupplierReturn
                {
                    TenantId = identity.TenantId,
                    CompanyId = identity.CompanyId,
                    ReturnId = getReturnId,
                    ProjectId = objModel.ProjectId,
                    SupplierId = objModel.SupplierId,
                    ReturnNo = $"INV-2025-00{getReturnId}",
                    ReturnDate = objModel.ReturnDate,
                    TotalAmount = objModel.TotalAmount,
                    TotalDiscount = objModel.TotalDiscount,
                    TotalTax = objModel.TotalTax,
                    NetAmount = objModel.NetAmount,
                    RoundOff = objModel.RoundOff,
                    Status = objModel.Status,
                    Remarks = objModel.Remarks,
                    IsActive = true,
                    CreatedBy = objModel.CreatedBy ?? "Admin",
                    CreatedOn = DateTime.Now
                };

                await _context.SupplierReturns.AddAsync(entity);
                await _context.SaveChangesAsync();  // Save to get ReturnId

                // Generate next PO Id
                var returnDetailId = (await _context.SupplierReturnDetails
                    .Where(x => x.TenantId == identity.TenantId && x.CompanyId == identity.CompanyId)
                    .Select(x => x.ReturnDetailId)
                    .ToListAsync())
                    .DefaultIfEmpty(0)
                    .Max() + 1;

                // ====== 2. Supplier Return Details insert ======
                foreach (var detail in objModel.SupplierReturnDetails)
                {
                    var detailEntity = new SupplierReturnDetail
                    {
                        TenantId = identity.TenantId,
                        CompanyId = identity.CompanyId,
                        ReturnDetailId = returnDetailId++,
                        ReturnId = getReturnId,
                        PoId = detail.PoId,
                        ItemId = detail.ItemId,
                        Qty = detail.Qty,
                        Price = detail.Price,
                        Total = detail.Qty * detail.Price,
                        Remarks = detail.Remarks,
                        IsActive = true,
                        CreatedBy = objModel.CreatedBy ?? "Admin",
                        CreatedOn = DateTime.Now
                    };

                    await _context.SupplierReturnDetails.AddAsync(detailEntity);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                // ====== 3. Map back to DTO ======
                objModel.ReturnId = entity.ReturnId;
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
