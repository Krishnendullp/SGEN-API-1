using AutoMapper;
using BillingService.Application.Interfaces;
using BillingService.Domain.Entities;
using Framework.Entities;
using Framework.Repositories;
using Framework.Services;
using Framework.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using SGen.Framework.Common;
using SGen.Framework.Contexts;
using SGen.Framework.Entities;
using SGen.Framework.Helper;
using SGen.Framework.Services;
using System;

namespace BillingService.Application.Services;

public class SaleInvoicePaymentService : CrudService<SaleInvoicePayment, SaleInvoicePaymentDto, long>, ISaleInvoicePaymentService
{
    private readonly SGenDbContext _context;
    private readonly IMapper _mapper;
    private readonly IIdentityServices _identityService;
    // _commandRepo and _repository are available from base CrudService if protected

    public SaleInvoicePaymentService(
        IQueryRepository<SaleInvoicePayment, long> repository,
        IMapper mapper,
        ICommandRepository<SaleInvoicePayment, long> commandRepo,
        SGenDbContext context,
        IIdentityServices identityService)
        : base(repository, mapper, commandRepo)
    {
        _context = context;
        _mapper = mapper;
        _identityService = identityService;
    }

    public override async Task<SaleInvoicePaymentDto> CreateAsync(SaleInvoicePaymentDto objModel)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var identity = _identityService.GetIdentity();
            identity.TenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            identity.CompanyId = Guid.Parse("22222222-2222-2222-2222-222222222222");

            var getPayId = (await _context.SaleInvoicePayments
                    .Where(x => x.TenantId == identity.TenantId && x.CompanyId == identity.CompanyId)
                    .Select(x => x.InvoicePaymentId)
                    .ToListAsync())
                    .DefaultIfEmpty(0)
                    .Max() + 1;

                var saleDetail = new SaleInvoicePayment
                {
                    TenantId = identity.TenantId,
                    CompanyId = identity.CompanyId,
                    InvoicePaymentId = getPayId,
                    SaleId = objModel.SaleId,
                    PayAmount = objModel.PayAmount,
                    PayDate = objModel.PayDate,
                    Remarks = objModel.Remarks,
                    PaymentMode = objModel.PaymentMode,
                    IsActive = true,
                    CreatedBy = objModel.CreatedBy ?? "Admin",
                    CreatedOn = DateTime.UtcNow
                };

                await _context.AddAsync(saleDetail);

            //Find Payment Id
            var getPo = await _context.SaleInvoicePayments
                .Where(x => x.TenantId == identity.TenantId &&
                                    x.CompanyId == identity.CompanyId
                                    && x.SaleId == objModel.SaleId && x.IsActive == true).ToListAsync();

            var totalPayAmount = getPo.Sum(x => x.PayAmount ?? 0) + (objModel.PayAmount ?? 0);

            var saleInvoice = await _context.SaleMasters
                .FirstOrDefaultAsync(x => x.TenantId == identity.TenantId &&
                                                 x.CompanyId == identity.CompanyId &&
                                                 x.Id == objModel.SaleId && true);


            if (saleInvoice?.TotalAmount == 0 || totalPayAmount == 0)
            {
                saleInvoice.Status = "Pending";
            }
            else if (totalPayAmount >= saleInvoice?.TotalAmount)
            {
                saleInvoice.Status = "Paid";
            }
            else if (totalPayAmount < saleInvoice?.TotalAmount)
            {
                saleInvoice.Status = "Partial Receive";
            }

            // Update payment received amount
            saleInvoice.PaymentReceiveAmount = totalPayAmount;

            saleInvoice.UpdatedBy = objModel.UpdatedBy ?? "Admin";
            saleInvoice.UpdatedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            var saleMaster = await _context.SaleMasters.Include(x => x.CustomerMasters)
                                                        .FirstOrDefaultAsync(x => x.TenantId == identity.TenantId &&
                                                        x.CompanyId == identity.CompanyId && x.IsActive == true
                                                        && x.Id == objModel.SaleId);

            string? customertName = saleMaster?.CustomerMasters?.CustomerName;

            var getLedger = await _context.LedgerMasters.Where(x => x.TenantId == identity.TenantId &&
                                                        x.CompanyId == identity.CompanyId && x.IsActive == true
                                                        && (x.LedgerName == objModel.PaymentMode || x.LedgerName == customertName)).ToListAsync();

            // debit ledger Id DR (Account)
            var paymentLedgerId = getLedger.Where(x => x.LedgerName == objModel.PaymentMode)
                                                .Select(x => (int?)x.LedgerId).FirstOrDefault();

            // credit ledger Id CR (Account)

            var custLedgerId = getLedger.Where(x => x.LedgerName == customertName)
                                           .Select(x => (int?)x.LedgerId).FirstOrDefault();

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
                LedgerId = paymentLedgerId,
                //SubLedgerId = paymentSubLedgerId,
                EntryType = "SALES",
                RefId = saleDetail.SaleId,
                AccountType = "DR",
                DebitAmount = (decimal)saleDetail.PayAmount,
                CreditAmount = 0,
                EntryDate = saleDetail.PayDate.Value.Date,
                Narration = $"Sales Invoice {saleDetail.InvoicePaymentId}",
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
                LedgerId = custLedgerId,
                //SubLedgerId = custSubLedgerId,
                EntryType = "SALES",
                RefId = saleDetail.SaleId,
                AccountType = "CR",
                DebitAmount = 0,
                CreditAmount = (decimal)saleDetail.PayAmount,
                EntryDate = saleDetail.PayDate.Value.Date,
                Narration = $"Sales Invoice {saleDetail.InvoicePaymentId}",
                IsActive = true,
                CreatedBy = objModel.CreatedBy ?? "admin",
                CreatedOn = DateTime.UtcNow
            };

            _context.LedgerDetails.Add(ledger);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            objModel.InvoicePaymentId = saleDetail.InvoicePaymentId;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception($"Error while creating sale invoice: {ex.Message}", ex);
        }
        return objModel;
    }

    public override async Task<PagedResult<IEnumerable<SaleInvoicePaymentDto>>> GetAllAsync(QueryParameters queryParams)
    {
        try
        {
            var identity = _identityService.GetIdentity();

            // Temporary test/demo tenant
            identity.TenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            identity.CompanyId = Guid.Parse("22222222-2222-2222-2222-222222222222");

            // ✅ Step 1: Build query with includes (specific to TaxGroup)
            var query = _context.Set<SaleInvoicePayment>()
                .AsNoTracking()
                .Include(tg => tg.SaleMasters)
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
            var dtoList = _mapper.Map<List<SaleInvoicePaymentDto>>(pagedData);

            // ✅ Step 7: Return consistent paged result
            return new PagedResult<IEnumerable<SaleInvoicePaymentDto>>
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
            return PagedResult<IEnumerable<SaleInvoicePaymentDto>>.Fail($"Error fetching TaxGroup: {ex.Message}");
        }
    }

    public override async Task<IEnumerable<SaleInvoicePaymentDto>> GetAllAsync()
    {
        try
        {
            var identity = _identityService.GetIdentity();

            // For testing/demo
            identity.TenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            identity.CompanyId = Guid.Parse("22222222-2222-2222-2222-222222222222");

            // ✅ Include related tables dynamically
            var query = _context.Set<SaleInvoicePayment>()    // Example 1
                .Include(x => x.SaleMasters)
                .Where(x =>
                    x.TenantId == identity.TenantId &&
                    x.CompanyId == identity.CompanyId &&
                    x.IsActive == true);

            //var entity = await query.FirstOrDefaultAsync();

            if (query == null)
                throw new Exception($"Purchase Order not found ");

            // ✅ Map to DTO before returning
            var dto = _mapper.Map<IEnumerable<SaleInvoicePaymentDto>>(query);
            return dto;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ItemService.GetByIdAsync] Error: {ex.Message}");
            throw;

        }
    }

    public override async Task<SaleInvoicePaymentDto> GetByIdAsync(int id)
    {
        try
        {
            var identity = _identityService.GetIdentity();

            // For testing/demo
            identity.TenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            identity.CompanyId = Guid.Parse("22222222-2222-2222-2222-222222222222");

            // ✅ Include related tables dynamically
            var query = _context.Set<SaleInvoicePayment>()    // Example 1
                .Include(x => x.SaleMasters)
                .Where(x =>
                    x.TenantId == identity.TenantId &&
                    x.CompanyId == identity.CompanyId &&
                    x.IsActive == true);

            //var entity = await query.FirstOrDefaultAsync();

            if (query == null)
                throw new Exception($"Purchase Order not found ");


            var entity = await query.FirstOrDefaultAsync();

            if (entity == null)
                throw new Exception($"Purchase Order not found with Id={id}");

            // ✅ Map to DTO before returning
            var dto = _mapper.Map<SaleInvoicePaymentDto>(entity);
            return dto;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ItemService.GetByIdAsync] Error: {ex.Message}");
            throw;
        }
    }
}