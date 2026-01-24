using System;
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
    public class VoucherService : CrudService<VoucherMaster, VoucherMasterDto, long>, IVoucherService
    {
        private readonly SGenDbContext _context;
        private readonly IMapper _mapper;
        private readonly IIdentityServices _identityService;
        public VoucherService(IQueryRepository<VoucherMaster, long> repository,
            SGenDbContext context,
            IMapper mapper,
            IIdentityServices identityService,
            ICommandRepository<VoucherMaster, long> commandRepo)
        : base(repository, mapper, commandRepo)
        {
            _context = context;
            _mapper = mapper;
            _identityService = identityService;
        }

        public override async Task<VoucherMasterDto> CreateAsync(VoucherMasterDto objModel)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var identity = _identityService.GetIdentity();
                identity.TenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");
                identity.CompanyId = Guid.Parse("22222222-2222-2222-2222-222222222222");

                // Generate next Payment Id
                var voucherId = (await _context.VoucherMasters
                    .Where(x => x.TenantId == identity.TenantId && x.CompanyId == identity.CompanyId)
                    .Select(x => x.VoucherId)
                    .ToListAsync())
                    .DefaultIfEmpty(0)
                    .Max() + 1;

                // 🔹 Master (SupplierPayment)
                var obj = new VoucherMaster
                {
                    TenantId = identity.TenantId,
                    CompanyId = identity.CompanyId,
                    VoucherId = voucherId,
                    VoucherNo = $"Vo-2025-00{voucherId}",
                    VoucherType = objModel.VoucherType,
                    VoucherDate = objModel.VoucherDate,
                    ReferenceNo = objModel.ReferenceNo,
                    Narration = objModel.Narration,
                    IsPosted = false,
                    IsCancelled = false,
                    IsActive = true,
                    CreatedBy = objModel.CreatedBy ?? "admin",
                    CreatedOn = DateTime.UtcNow
                };

                _context.VoucherMasters.Add(obj);

                if (objModel.LedgerDetails != null && objModel.LedgerDetails.Any())
                {
                    var ledgerDetailId = (await _context.LedgerDetails
                    .Where(x => x.TenantId == identity.TenantId && x.CompanyId == identity.CompanyId)
                    .Select(x => x.Id)
                    .ToListAsync())
                    .DefaultIfEmpty(0)
                    .Max() + 1;

                    foreach (var detail in objModel.LedgerDetails)
                    {
                        var ledgerDetail = new LedgerDetail
                        {
                            TenantId = identity.TenantId,
                            CompanyId = identity.CompanyId,
                            Id = ledgerDetailId++,
                            LedgerId = 12,
                            SubLedgerId = detail.SubLedgerId,
                            EntryType = objModel.VoucherType,
                            RefId = obj.VoucherId,
                            AccountType = "DR",
                            DebitAmount = detail.DebitAmount,
                            CreditAmount = detail.CreditAmount,
                            EntryDate = objModel.VoucherDate.Value.Date,
                            Narration = $"Voucher  {obj.VoucherId}",
                            IsActive = true,
                            CreatedBy = objModel.CreatedBy ?? "admin",
                            CreatedOn = DateTime.UtcNow
                        };

                        _context.LedgerDetails.Add(ledgerDetail);
                    }
                }

                // ✅ একবারেই SaveChangesAsync
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                objModel.VoucherId = obj.VoucherId;
                return objModel;
            }
            catch (Exception ex) 
            {
                await transaction.RollbackAsync();
                throw ex;

            }

        }

    //    public override async Task<VoucherMasterDto> UpdateAsync(VoucherMasterDto objModel)
    //    {
    //        using var transaction = await _context.Database.BeginTransactionAsync();

    //        try
    //        {
    //            // 🔹 Master (Voucher Master) find
    //            var obj = await _context.VoucherMasters
    //                .FirstOrDefaultAsync(x => x.TenantId == objModel.TenantId
    //                                       && x.CompanyId == objModel.CompanyId
    //                                       && x.VoucherId == objModel.VoucherId
    //                                       && x.IsActive == true);

    //            if (obj == null)
    //                return null; // Not found

    //            // 🔹 Update Master fields
    //            obj.VoucherNo = $"Vo-2025-00{obj.VoucherId}";
    //            obj.VoucherType = objModel.VoucherType;
    //            obj.VoucherDate = objModel.VoucherDate;
    //            obj.ReferenceNo = objModel.ReferenceNo;
    //            obj.Narration = objModel.Narration;
    //            obj.IsPosted = false;
    //            obj.IsCancelled = false;
    //            obj.IsActive = true;
    //            obj.UpdatedBy = "Admin";
    //            obj.UpdatedOn = DateTime.UtcNow;


    //            _context.VoucherMasters.Update(obj);

    //            if (objModel.LedgerDetails != null && objModel.LedgerDetails.Any())
    //            {
    //                // 🔹 Existing Ledger Details (with taxes)
    //                var existingDetails = await _context.LedgerDetails
    //.Where(x => x.TenantId == objModel.TenantId
    //         && x.CompanyId == objModel.CompanyId
    //         && x.RefId == (long)objModel.VoucherId)
    //.ToListAsync();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            await transaction.RollbackAsync();
    //            throw ex;
    //        }
    //    }
    }   
}
