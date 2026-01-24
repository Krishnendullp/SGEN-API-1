using AutoMapper;
using BillingService.Application.Interfaces;
using BillingService.Domain.Entities;
using Framework.Entities;
using Framework.Repositories;
using Framework.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using SGen.Framework.Common;
using SGen.Framework.Contexts;
using SGen.Framework.Entities;
using SGen.Framework.Helper;
using SGen.Framework.Services;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BillingService.Application.Services;

public class SaleMasterService : CrudService<SaleMaster, SaleMasterDto, long>, ISaleMasterService
{
    private readonly SGenDbContext _context;
    private readonly IMapper _mapper;
    private readonly IIdentityServices _identityService;
    // _commandRepo and _repository are available from base CrudService if protected

    public SaleMasterService(
        IQueryRepository<SaleMaster, long> repository,
        IMapper mapper,
        ICommandRepository<SaleMaster, long> commandRepo,
        SGenDbContext context,
        IIdentityServices identityService)
        : base(repository, mapper, commandRepo)
    {
        _context = context;
        _mapper = mapper;
        _identityService = identityService;
    }

    public override async Task<SaleMasterDto> CreateAsync(SaleMasterDto objModel)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var identity = _identityService.GetIdentity();
            identity.TenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            identity.CompanyId = Guid.Parse("22222222-2222-2222-2222-222222222222");

            // Generate next Sale Id
            var getId = (await _context.SaleMasters
                .Where(x => x.TenantId == identity.TenantId && x.CompanyId == identity.CompanyId)
                .Select(x => x.Id)
                .ToListAsync())
                .DefaultIfEmpty(0)
                .Max() + 1;

            // 🔹 Master (SaleMaster)
            var obj = new SaleMaster
            {
                TenantId = identity.TenantId,
                CompanyId = identity.CompanyId,
                Id = getId,
                ProjectId = objModel.ProjectId,
                CustomerId = objModel.CustomerId,
                InvNo = $"INV-2025-00{getId}",
                SaleDate = objModel.SaleDate.Date,
                TaxableAmount = objModel.TaxableAmount,
                TotalTax = objModel.TotalTax,
                TotalAmount = objModel.TotalAmount,
                DiscountAmount = objModel.DiscountAmount,
                Discount = objModel.Discount,
                FinYr = objModel.FinYr,
                PaymentReceiveAmount = objModel.PaymentReceiveAmount,
                ItemTax = objModel.ItemTax,
                NetAmount = objModel.NetAmount,
                TaxGroupId = objModel.TaxGroupId == 0 ? 25 : objModel.TaxGroupId,
                Status = objModel.Status,
                IsActive = true,
                CreatedBy = objModel.CreatedBy ?? "admin",
                CreatedOn = DateTime.UtcNow
            };

            _context.SaleMasters.Add(obj);
            //await _context.SaveChangesAsync();

            if (objModel.SaleMasterTaxs != null && objModel.SaleMasterTaxs.Any())
            {
                var getDetailId = (await _context.SaleMasterTaxs
                    .Where(x => x.TenantId == identity.TenantId && x.CompanyId == identity.CompanyId)
                    .Select(x => x.Id)
                    .ToListAsync())
                    .DefaultIfEmpty(0)
                    .Max() + 1;

                foreach (var detail in objModel.SaleMasterTaxs)
                {
                    var SaleMasterTax = new SaleMasterTax
                    {
                        TenantId = identity.TenantId,
                        CompanyId = identity.CompanyId,
                        Id = getDetailId++,
                        SaleId = getId,
                        TaxPercentage = detail.TaxPercentage,
                        TaxAmount = detail.TaxAmount,
                        IsActive = true,
                        CreatedBy = objModel.CreatedBy ?? "admin",
                        CreatedOn = DateTime.UtcNow
                    };

                    _context.SaleMasterTaxs.Add(SaleMasterTax);
                    await _context.SaveChangesAsync();
                }
            }
            // 🔹 Details (SaleDetails + SaleDetailTaxes)
            if (objModel.SaleMasterDetails != null && objModel.SaleMasterDetails.Any())
            {
                var getDetailId = (await _context.SaleMasterDetails
                    .Where(x => x.TenantId == identity.TenantId && x.CompanyId == identity.CompanyId)
                    .Select(x => x.Id)
                    .ToListAsync())
                    .DefaultIfEmpty(0)
                    .Max() + 1;

                var getSaleTaxId = (await _context.SaleDetailTaxs
                    .Where(x => x.TenantId == identity.TenantId && x.CompanyId == identity.CompanyId)
                    .Select(x => x.Id)
                    .ToListAsync())
                    .DefaultIfEmpty(0)
                    .Max() + 1;

                foreach (var detail in objModel.SaleMasterDetails)
                {
                    var SaleMasterDetail = new SaleMasterDetail
                    {
                        TenantId = identity.TenantId,
                        CompanyId = identity.CompanyId,
                        Id = getDetailId++,
                        SaleId = getId,
                        ItemId = detail.ItemId,
                        TaxGroupId = detail.TaxGroupId,
                        TaxableAmount = detail.TaxableAmount,
                        TotalTax = detail.TotalTax,
                        Total = detail.Total,
                        IsActive = true,
                        Rate = detail.Rate,
                        CreatedBy = objModel.CreatedBy ?? "admin",
                        CreatedOn = DateTime.UtcNow
                    };

                    _context.SaleMasterDetails.Add(SaleMasterDetail);

                    // Child (Taxes)
                    if (detail.SaleDetailTaxs!= null && detail.SaleDetailTaxs.Any())
                    {
                        foreach (var tax in detail.SaleDetailTaxs)
                        {
                            var taxEntity = new SaleDetailTax
                            {
                                TenantId = identity.TenantId,
                                CompanyId = identity.CompanyId,
                                Id = getSaleTaxId++,
                                SaleDetailId = SaleMasterDetail.Id, // ✅ correct parent reference
                                TaxId = tax.TaxId,
                                TaxPercentage = tax.TaxPercentage,
                                TaxAmount = tax.TaxAmount,
                                IsActive = tax.IsActive,
                                CreatedBy = objModel.CreatedBy ?? "admin",
                                CreatedOn = DateTime.UtcNow
                            };

                            _context.SaleDetailTaxs.Add(taxEntity);
                        }
                        await _context.SaveChangesAsync();
                    }
                }
            }

            var subLedger = await _context.SubLedgers.FirstOrDefaultAsync(x =>x.TenantId == identity.TenantId &&
                                                        x.CompanyId == identity.CompanyId && x.IsActive == true
                                                        &&x.SubLedgerName == objModel.CustomerMasters.CustomerName);

            int? subLedgerId = subLedger?.SubLedgerId;

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
                LedgerId = 12,
                SubLedgerId = subLedgerId,
                EntryType = "SALES",
                RefId = obj.Id,
                AccountType = "DR",
                DebitAmount = obj.NetAmount,
                CreditAmount = 0,
                EntryDate = obj.SaleDate,
                Narration = $"Sales Invoice {obj.InvNo}",
                IsActive = true,
                CreatedBy = objModel.CreatedBy ?? "admin",
                CreatedOn = DateTime.UtcNow
            };

            _context.LedgerDetails.Add(objLedger);

            var ledger = new LedgerDetail
            {
                TenantId = identity.TenantId,
                CompanyId = identity.CompanyId,
                Id = ledgerDetailId++,
                LedgerId = 12,
                SubLedgerId = subLedgerId,
                EntryType = "SALES",
                RefId = obj.Id,
                AccountType = "CR",
                DebitAmount = 0,
                CreditAmount = obj.NetAmount,
                EntryDate = obj.SaleDate,
                Narration = $"Sales Invoice {obj.InvNo}",
                IsActive = true,
                CreatedBy = objModel.CreatedBy ?? "admin",
                CreatedOn = DateTime.UtcNow
            };

            _context.LedgerDetails.Add(ledger);

            // ✅ একবারেই SaveChangesAsync
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            objModel.Id = obj.Id;
            return objModel;
        }
        catch(Exception ex)
        {
            await transaction.RollbackAsync();
            throw ex;
        }
    }

    public override async Task<PagedResult<IEnumerable<SaleMasterDto>>> GetAllAsync(QueryParameters queryParams)
    {
        try
        {
            var identity = _identityService.GetIdentity();

            // Temporary test/demo tenant
            identity.TenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            identity.CompanyId = Guid.Parse("22222222-2222-2222-2222-222222222222");

            // ✅ Step 1: Build query with includes (specific to TaxGroup)
            var query = _context.Set<SaleMaster>()
                .AsNoTracking()
                .Include(cs => cs.CustomerMasters)
                .Include(pr => pr.ProjectMasters)
                .Include(tg => tg.TaxGroups)
                .Include(tm => tm.SaleMasterTaxs)
                .Include(tg => tg.SaleMasterDetails)
                .ThenInclude(x => x.TaxGroups)
                .Include(tg => tg.SaleMasterDetails)
                    .ThenInclude(tx => tx.SaleDetailTaxs)
                    .ThenInclude(ts => ts.Taxs)
                .Where(x => x.TenantId == identity.TenantId &&
                            x.CompanyId == identity.CompanyId &&
                            x.IsActive == true);

            // ✅ Step 2: Apply filters and sorting dynamically
            query = query.ApplyFilters(queryParams.Filters)
                         .ApplySorting(queryParams.Sorts);

            // ✅ Step 3: Handle invalid pagination input
            if (queryParams.PageNo <= 0)
                queryParams.PageNo = 1;
            if (queryParams.PageSize <= 0)
                queryParams.PageSize = 100;

            // ✅ Step 4: Get total count
            var totalCount = await query.CountAsync();

            // ✅ Step 5: Get paged data
            var pagedData = await query
                .Skip((queryParams.PageNo - 1) * queryParams.PageSize)
                .Take(queryParams.PageSize)
                .ToListAsync();

            // ✅ Step 6: Map to DTO
            var dtoList = _mapper.Map<List<SaleMasterDto>>(pagedData);

            // ✅ Step 7: Return consistent paged result
            return new PagedResult<IEnumerable<SaleMasterDto>>
            {
                Data = dtoList,
                PageNo = queryParams.PageNo,
                PageSize = queryParams.PageSize,
                TotalCount = totalCount,
                Success = dtoList.Any(),
                Message = dtoList.Any() ? "Records fetched successfully." : "No records found."
            };
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex, "Error fetching TaxGroup");
            return PagedResult<IEnumerable<SaleMasterDto>>.Fail($"Error fetching TaxGroup: {ex.Message}");
        }
    }

    public override async Task<IEnumerable<SaleMasterDto>> GetAllAsync()
    {
        try
        {
            var identity = _identityService.GetIdentity();

            // Demo tenant/company (remove in production)
            identity.TenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            identity.CompanyId = Guid.Parse("22222222-2222-2222-2222-222222222222");

            // ⛔ ভুল: ToListAsync() await করা হয়নি
            var entityList = await _context.Set<SaleMaster>()
                .AsNoTracking()
                .Include(cs => cs.CustomerMasters)
                .Include(pr => pr.ProjectMasters)
                .ThenInclude(x => x.PurchaseOrders)
                .ThenInclude(x => x.SupplierMasters)
                .Include(tg => tg.TaxGroups)
                .Include(tm => tm.SaleMasterTaxs)
                .Include(tg => tg.SaleMasterDetails)
                    .ThenInclude(tx => tx.SaleDetailTaxs)
                    .ThenInclude(ts => ts.Taxs)
                .Where(x =>
                    x.TenantId == identity.TenantId &&
                    x.CompanyId == identity.CompanyId &&
                    x.IsActive)
                .ToListAsync();  // ✅ await দিতে হবে

            if (entityList == null || !entityList.Any())
                throw new Exception("No sale records found.");

            // 🔥 Map actual list
            var dto = _mapper.Map<IEnumerable<SaleMasterDto>>(entityList);
            return dto;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public override async Task<SaleMasterDto> GetByIdAsync(int id)
    {
        try
        {
            var identity = _identityService.GetIdentity();

            // For testing/demo
            identity.TenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            identity.CompanyId = Guid.Parse("22222222-2222-2222-2222-222222222222");

            // ✅ Include related tables dynamically
            var query = _context.Set<SaleMaster>()    // Example 1
                .AsNoTracking()
                .Include(cs => cs.CustomerMasters)
                .Include(pr => pr.ProjectMasters)
                .Include(tg => tg.TaxGroups)
                .Include(tm => tm.SaleMasterTaxs)
                .Include(tg => tg.SaleMasterDetails)
                .ThenInclude(it => it.TaxGroups)
                    .ThenInclude(tx => tx.TaxGroupIgstDetails)
                        .ThenInclude(st => st.Taxs)
                .Include(tg => tg.SaleMasterDetails)
                    .ThenInclude(it => it.TaxGroups)
                        .ThenInclude(tx => tx.TaxGroupSgstDetails)
                            .ThenInclude(st => st.Taxs)
                .Include(tg => tg.SaleMasterDetails)
                    .ThenInclude(tx => tx.SaleDetailTaxs)
                    .ThenInclude(ts => ts.Taxs)
                .Where(x =>
                    x.TenantId == identity.TenantId &&
                    x.CompanyId == identity.CompanyId &&
                    x.IsActive == true &&
                    x.Id == id);

            var entity = await query.FirstOrDefaultAsync();

            if (query == null)
                throw new Exception($"Purchase Order not found with Id={id}");

            // ✅ Map to DTO before returning
            var dto = _mapper.Map<SaleMasterDto>(entity);
            return dto;
        }
        catch (Exception ex)
        {
            //Console.WriteLine($"[ItemService.GetByIdAsync] Error: {ex.Message}");
            throw;
        }
    }

}
