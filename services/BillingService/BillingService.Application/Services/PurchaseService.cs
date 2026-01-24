using AutoMapper;
using BillingService.Application.Interfaces;
using BillingService.Domain.Entities;
using Framework.Common;
using Framework.Entities;
using Framework.Repositories;
using Framework.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using SGen.Framework.Contexts;
using SGen.Framework.Entities;
using SGen.Framework.Services;
using System.Data;

namespace BillingService.Application.Services;

public class PurchaseService : CrudService<PurchaseOrder, PurchaseOrderDto, long>, IPurchaseService
{
    private readonly SGenDbContext _context;
    private readonly IMapper _mapper;
    private readonly IIdentityServices _identityService;

    //_commandRepo and _repository are available from base CrudService if protected

    public PurchaseService(
        IQueryRepository<PurchaseOrder, long> repository,
        IMapper mapper,
        ICommandRepository<PurchaseOrder, long> commandRepo,
        SGenDbContext context,
        IIdentityServices identityService)
        : base(repository, mapper, commandRepo)
    {
        _context = context;
        _mapper = mapper;
        _identityService = identityService;
    }


    public override async Task<PurchaseOrderDto> CreateAsync(PurchaseOrderDto objModel)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var identity = _identityService.GetIdentity();
            identity.TenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            identity.CompanyId = Guid.Parse("22222222-2222-2222-2222-222222222222");

            // Generate next PO Id
            var poId = (await _context.PurchaseOrders
                .Where(x => x.TenantId == identity.TenantId 
                && x.CompanyId == identity.CompanyId)
                .Select(x => x.PoId)
                .ToListAsync())
                .DefaultIfEmpty(0)
                .Max() + 1;

            // 🔹 Master (PurchaseOrder)
            var obj = new PurchaseOrder
            {
                TenantId = identity.TenantId,
                CompanyId = identity.CompanyId,
                PoId = poId,
                ProjectId = objModel.ProjectId,
                SupplierId = objModel.SupplierId,
                PoNo = $"PO-2025-00{poId}",
                PoDate = objModel.PoDate ?? DateTime.UtcNow,
                TotalTaxableAmount = objModel.TotalTaxableAmount,
                TotalDiscount = objModel.TotalDiscount,
                TotalTax = objModel.TotalTax,
                NetAmount = objModel.NetAmount,
                RoundOff = objModel.RoundOff,
                Status = objModel.Status ?? "Pending",
                Remarks = objModel.Remarks,
                FinYear = objModel.FinYear,
                DueAmount = objModel.DueAmount,
                IsActive = true,
                CreatedBy = objModel.CreatedBy ?? "admin",
                CreatedOn = DateTime.UtcNow
            };

            _context.PurchaseOrders.Add(obj);

            // 🔹 Details (StoreTransactions + StoreTransactionTaxes)
            if (objModel.StoreTransactions != null && objModel.StoreTransactions.Any())
            {
                var storeId = (await _context.StoreTransactions
                    .Where(x => x.TenantId == identity.TenantId && x.CompanyId == identity.CompanyId)
                    .Select(x => x.StoreId)
                    .ToListAsync())
                    .DefaultIfEmpty(0)
                    .Max() + 1;

                var storeTaxId = (await _context.StoreTransactionTaxes
                    .Where(x => x.TenantId == identity.TenantId && x.CompanyId == identity.CompanyId)
                    .Select(x => x.StoreTaxId)
                    .ToListAsync())
                    .DefaultIfEmpty(0)
                    .Max() + 1;

                foreach (var detail in objModel.StoreTransactions)
                {
                    var storeTransaction = new StoreTransaction
                    {
                        TenantId = identity.TenantId,
                        CompanyId = identity.CompanyId,
                        StoreId = storeId++,
                        PoId = poId,
                        ItemId = detail.ItemId,
                        UnitId = detail.UnitId,
                        TaxGroupId = detail.TaxGroupId,
                        TransactionType = detail.TransactionType ?? "Purchase",
                        TransactionDate = detail.TransactionDate ?? DateTime.UtcNow,
                        Qty = detail.Qty ?? 0,
                        Rate = detail.Rate ?? 0,
                        NetAmount = detail.NetAmount ?? 0,
                        Remarks = detail.Remarks,
                        TotalTaxableAmount = detail.TotalTaxableAmount ?? 0,
                        TotalTax = detail.TotalTax ?? 0,
                        IsActive = true,
                        CreatedBy = objModel.CreatedBy ?? "admin",
                        CreatedOn = DateTime.UtcNow
                    };

                    _context.StoreTransactions.Add(storeTransaction);

                    // Child (Taxes)
                    if (detail.StoreTransactionTaxes != null && detail.StoreTransactionTaxes.Any())
                    {
                        foreach (var tax in detail.StoreTransactionTaxes)
                        {
                            var taxEntity = new StoreTransactionTax
                            {
                                TenantId = identity.TenantId,
                                CompanyId = identity.CompanyId,
                                StoreTaxId = storeTaxId++,
                                StoreId = storeTransaction.StoreId, // ✅ correct parent reference
                                TaxId = tax.TaxId,
                                TaxPercentage = tax.TaxPercentage,
                                TaxAmount = tax.TaxAmount,
                                IsActive = tax.IsActive,
                                CreatedBy = objModel.CreatedBy ?? "admin",
                                CreatedOn = DateTime.UtcNow
                            };

                            _context.StoreTransactionTaxes.Add(taxEntity);
                        }
                    }
                }
            }

            //var getLedger = await _context.LedgerMasters.FirstOrDefaultAsync(x => x.TenantId == identity.TenantId &&
            //                                            x.CompanyId == identity.CompanyId && x.IsActive == true
            //                                            && x.LedgerName == objModel.SupplierMasters.SupplierName);

            var ledgerAll = await _context.LedgerMasters.Where(x => x.TenantId == identity.TenantId
                                       && x.CompanyId == identity.CompanyId && x.IsActive == true).ToListAsync();

            var getLedger = ledgerAll.FirstOrDefault(x => x.LedgerName == objModel.SupplierMasters.SupplierName);

            int ? ledgerId = getLedger?.LedgerId;

            var ledgerDetailId = (await _context.LedgerDetails
            .Where(x => x.TenantId == identity.TenantId && x.CompanyId == identity.CompanyId)
            .Select(x => x.Id)
            .ToListAsync())
            .DefaultIfEmpty(0)
            .Max() + 1;

            var objLedger = new LedgerDetail
            {
                TenantId = identity.TenantId,
                CompanyId = identity.CompanyId,
                Id = ledgerDetailId++,
                LedgerId = 3,
                //SubLedgerId = subLedgerId,
                EntryType = "Purchase",
                RefId = obj.PoId,
                AccountType = "DR",
                DebitAmount = (decimal)obj.NetAmount,
                CreditAmount = 0,
                EntryDate = obj.PoDate.Value.Date,
                Narration = $"Sales Invoice {obj.PoNo}",
                IsActive = true,
                CreatedBy = objModel.CreatedBy ?? "admin",
                CreatedOn = DateTime.UtcNow
            };
            _context.LedgerDetails.Add(objLedger);

            foreach (var st in objModel.StoreTransactions)
            {
                if (st.StoreTransactionTaxes == null) continue;

                foreach (var tax in st.StoreTransactionTaxes)
                {
                    // Tax Ledger name example: "Input SGST", "Input CGST"
                    var taxLedger = this._context.Taxes.FirstOrDefault(x =>
                        x.TaxId == tax.TaxId);

                    var getTaxLedger = ledgerAll.FirstOrDefault(x => x.LedgerName == taxLedger.Name);
                    int? ledgerTaxId = getTaxLedger?.LedgerId;

                    if (taxLedger == null) continue;

                    _context.LedgerDetails.Add(new LedgerDetail
                    {
                        TenantId = identity.TenantId,
                        CompanyId = identity.CompanyId,
                        Id = ledgerDetailId++,
                        LedgerId = ledgerTaxId,
                        EntryType = "Purchase",
                        RefId = obj.PoId,
                        AccountType = "DR",
                        DebitAmount = tax.TaxAmount ?? 0,
                        CreditAmount = 0,
                        EntryDate = obj.PoDate.Value.Date,
                        Narration = $"GST for PO {obj.PoNo}",
                        IsActive = true,
                        CreatedBy = objModel.CreatedBy ?? "admin",
                        CreatedOn = DateTime.UtcNow
                    });
                }
            }


            var ledger = new LedgerDetail
            {
                TenantId = identity.TenantId,
                CompanyId = identity.CompanyId,
                Id = ledgerDetailId++,
                LedgerId = ledgerId,
                //SubLedgerId = subLedgerId,
                EntryType = "Purchase",
                RefId = obj.PoId,
                AccountType = "CR",
                DebitAmount = 0,
                CreditAmount = (decimal)obj.NetAmount,
                EntryDate = obj.PoDate.Value.Date,
                Narration = $"Sales Invoice {obj.PoNo}",
                IsActive = true,
                CreatedBy = objModel.CreatedBy ?? "admin",
                CreatedOn = DateTime.UtcNow
            };

            _context.LedgerDetails.Add(ledger);

            // ✅ একবারেই SaveChangesAsync
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            objModel.PoId = obj.PoId;
            return objModel;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public override async Task<PurchaseOrderDto> GetByIdAsync(int id)
    {
        try
        {
            var identity = _identityService.GetIdentity();

            // For testing/demo
            identity.TenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            identity.CompanyId = Guid.Parse("22222222-2222-2222-2222-222222222222");

            // ✅ Include related tables dynamically
            var query = _context.Set<PurchaseOrder>()    // Example 1
                .Include(x => x.StoreTransactions).ThenInclude(m => m.StoreTransactionTaxes)
                .Include(x => x.StoreTransactions).ThenInclude(i => i.ItemMaster)
                .Include(p => p.ProjectMasters)
                .Include(s => s.SupplierMasters)
                .Where(x =>
                    x.TenantId == identity.TenantId &&
                    x.CompanyId == identity.CompanyId &&
                    x.IsActive == true &&
                    x.PoId == id);

            var entity = await query.FirstOrDefaultAsync();

            if (entity == null)
                throw new Exception($"Purchase Order not found with Id={id}");

            // ✅ Map to DTO before returning
            var dto = _mapper.Map<PurchaseOrderDto>(entity);
            return dto;
        }
        catch (Exception ex)
        {
            //Console.WriteLine($"[ItemService.GetByIdAsync] Error: {ex.Message}");
            throw;
        }
    }

    public override async Task<PurchaseOrderDto> UpdateAsync(PurchaseOrderDto objModel)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            // 🔹 Master (PurchaseOrder) find
            var obj = await _context.PurchaseOrders
                .FirstOrDefaultAsync(x => x.TenantId == objModel.TenantId
                                       && x.CompanyId == objModel.CompanyId
                                       && x.PoId == objModel.PoId
                                       && x.IsActive == true);

            if (obj == null)
                return null; // Not found

            // 🔹 Update Master fields
            obj.ProjectId = objModel.ProjectId;
            obj.SupplierId = objModel.SupplierId;
            obj.PoDate = objModel.PoDate ?? obj.PoDate;
            obj.TotalTaxableAmount = objModel.TotalTaxableAmount;
            obj.TotalDiscount = objModel.TotalDiscount;
            obj.TotalTax = objModel.TotalTax;
            obj.NetAmount = objModel.NetAmount;
            obj.RoundOff = objModel.RoundOff;
            obj.Status = objModel.Status ?? obj.Status;
            obj.Remarks = objModel.Remarks;
            obj.FinYear = objModel.FinYear;
            obj.DueAmount = objModel.DueAmount;
            obj.UpdatedBy = objModel.UpdatedBy ?? "admin";
            obj.UpdatedOn = DateTime.UtcNow;

            _context.PurchaseOrders.Update(obj);
            await _context.SaveChangesAsync();

            // 🔹 Existing store transactions (with taxes)
            var existingDetails = await _context.StoreTransactions
                .Where(x => x.TenantId == objModel.TenantId
                         && x.CompanyId == objModel.CompanyId
                         && x.PoId == objModel.PoId)
                .Include(x => x.StoreTransactionTaxes)
                .ToListAsync();

            // --------------------
            // 🔹 Sync StoreTransactions
            // --------------------
            if (objModel.StoreTransactions != null && objModel.StoreTransactions.Any())
            {
                foreach (var detail in objModel.StoreTransactions)
                {
                    var existingDetail = existingDetails
                        .FirstOrDefault(d => d.StoreId == detail.StoreId);

                    if (existingDetail != null)
                    {
                        // ✅ Update existing
                        existingDetail.ItemId = detail.ItemId;
                        existingDetail.TaxGroupId = detail.TaxGroupId;
                        existingDetail.TransactionType = detail.TransactionType ?? existingDetail.TransactionType;
                        existingDetail.TransactionDate = detail.TransactionDate ?? existingDetail.TransactionDate;
                        existingDetail.Qty = detail.Qty ?? existingDetail.Qty;
                        existingDetail.NetAmount = detail.NetAmount ?? existingDetail.NetAmount;
                        existingDetail.Rate = detail.Rate ?? existingDetail.Rate;
                        existingDetail.Remarks = detail.Remarks;
                        existingDetail.IsActive = true; // reactivate if inactive
                        existingDetail.UpdatedBy = objModel.UpdatedBy ?? "admin";
                        existingDetail.UpdatedOn = DateTime.UtcNow;

                        // ✅ Sync taxes
                        var existingTaxes = existingDetail.StoreTransactionTaxes.ToList();

                        foreach (var taxDto in detail.StoreTransactionTaxes ?? new List<StoreTransactionTaxDto>())
                        {
                            var existingTax = existingTaxes
                                .FirstOrDefault(t => t.StoreTaxId == taxDto.StoreTaxId);

                            if (existingTax != null)
                            {
                                // Update existing tax
                                existingTax.TaxId = taxDto.TaxId;
                                existingTax.TaxPercentage = taxDto.TaxPercentage;
                                existingTax.TaxAmount = taxDto.TaxAmount;
                                existingTax.IsActive = true; // reactivate
                                existingTax.UpdatedBy = objModel.UpdatedBy ?? "admin";
                                existingTax.UpdatedOn = DateTime.UtcNow;
                            }
                            else
                            {
                                // Insert new tax
                                var newStoreTaxId = (await _context.StoreTransactionTaxes
                                    .Where(x => x.TenantId == objModel.TenantId && x.CompanyId == objModel.CompanyId)
                                    .Select(x => x.StoreTaxId)
                                    .DefaultIfEmpty(0)
                                    .MaxAsync()) + 1;

                                var newTax = new StoreTransactionTax
                                {
                                    TenantId = objModel.TenantId,
                                    CompanyId = objModel.CompanyId,
                                    StoreTaxId = newStoreTaxId,
                                    StoreId = existingDetail.StoreId,
                                    TaxId = taxDto.TaxId,
                                    TaxPercentage = taxDto.TaxPercentage,
                                    TaxAmount = taxDto.TaxAmount,
                                    IsActive = true,
                                    CreatedBy = objModel.CreatedBy ?? "admin",
                                    CreatedOn = DateTime.UtcNow
                                };

                                _context.StoreTransactionTaxes.Add(newTax);
                            }
                        }

                        // ❌ Mark removed taxes as inactive instead of deleting
                        foreach (var oldTax in existingTaxes)
                        {
                            if (!(detail.StoreTransactionTaxes?.Any(t => t.StoreTaxId == oldTax.StoreTaxId) ?? false))
                            {
                                oldTax.IsActive = false;
                                oldTax.UpdatedBy = objModel.UpdatedBy ?? "admin";
                                oldTax.UpdatedOn = DateTime.UtcNow;
                            }
                        }
                    }
                    else
                    {
                        // ✅ Insert new store transaction
                        var newStoreId = (await _context.StoreTransactions
                            .Where(x => x.TenantId == objModel.TenantId && x.CompanyId == objModel.CompanyId)
                            .Select(x => x.StoreId)
                            .DefaultIfEmpty(0)
                            .MaxAsync()) + 1;

                        var newDetail = new StoreTransaction
                        {
                            TenantId = objModel.TenantId,
                            CompanyId = objModel.CompanyId,
                            StoreId = newStoreId,
                            PoId = obj.PoId,
                            ItemId = detail.ItemId,
                            TaxGroupId = detail.TaxGroupId,
                            TransactionType = detail.TransactionType ?? "Purchase",
                            TransactionDate = detail.TransactionDate ?? DateTime.UtcNow,
                            Qty = detail.Qty ?? 0,
                            NetAmount = detail.NetAmount ?? 0,
                            Rate = detail.Rate ?? 0,
                            Remarks = detail.Remarks,
                            IsActive = true,
                            CreatedBy = objModel.CreatedBy ?? "admin",
                            CreatedOn = DateTime.UtcNow
                        };

                        _context.StoreTransactions.Add(newDetail);
                        await _context.SaveChangesAsync();

                        // Insert new taxes
                        foreach (var taxDto in detail.StoreTransactionTaxes ?? new List<StoreTransactionTaxDto>())
                        {
                            var newStoreTaxId = (await _context.StoreTransactionTaxes
                                .Where(x => x.TenantId == objModel.TenantId && x.CompanyId == objModel.CompanyId)
                                .Select(x => x.StoreTaxId)
                                .DefaultIfEmpty(0)
                                .MaxAsync()) + 1;

                            var taxEntity = new StoreTransactionTax
                            {
                                TenantId = objModel.TenantId,
                                CompanyId = objModel.CompanyId,
                                StoreTaxId = newStoreTaxId,
                                StoreId = newDetail.StoreId,
                                TaxId = taxDto.TaxId,
                                TaxPercentage = taxDto.TaxPercentage,
                                TaxAmount = taxDto.TaxAmount,
                                IsActive = true,
                                CreatedBy = objModel.CreatedBy ?? "admin",
                                CreatedOn = DateTime.UtcNow
                            };

                            _context.StoreTransactionTaxes.Add(taxEntity);
                        }
                    }
                }

                // ❌ Mark removed details as inactive instead of deleting
                foreach (var oldDetail in existingDetails)
                {
                    if (!objModel.StoreTransactions.Any(d => d.StoreId == oldDetail.StoreId))
                    {
                        oldDetail.IsActive = false;
                        oldDetail.UpdatedBy = objModel.UpdatedBy ?? "admin";
                        oldDetail.UpdatedOn = DateTime.UtcNow;
                    }
                }
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return objModel;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

    }

    public override async Task<IEnumerable<PurchaseOrderDto>> GetAllAsync()
    {
        try
        {
            var identity = _identityService.GetIdentity();

            // For testing/demo
            identity.TenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            identity.CompanyId = Guid.Parse("22222222-2222-2222-2222-222222222222");

            // ✅ Include related tables dynamically
            var query = _context.Set<PurchaseOrder>()    // Example 1
                .Include(x => x.StoreTransactions).ThenInclude(m => m.StoreTransactionTaxes).ThenInclude(st => st.Taxs)
                .Include(x => x.StoreTransactions).ThenInclude(i => i.ItemMaster)
                .Include(p => p.ProjectMasters)
                .Include(s => s.SupplierMasters)
                .Where(x =>
                    x.TenantId == identity.TenantId &&
                    x.CompanyId == identity.CompanyId &&
                    x.IsActive == true);

            //var entity = await query.FirstOrDefaultAsync();

            if (query == null)
                throw new Exception($"Purchase Order not found ");

            // ✅ Map to DTO before returning
            var dto = _mapper.Map<IEnumerable<PurchaseOrderDto>>(query);
            return dto ;
        }
        catch (Exception ex)
        {
            //Console.WriteLine($"[ItemService.GetByIdAsync] Error: {ex.Message}");
            throw;
        }

    }
}

