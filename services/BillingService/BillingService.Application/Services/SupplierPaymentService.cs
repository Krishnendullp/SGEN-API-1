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

public class SupplierPaymentService : CrudService<SupplierPayment, SupplierPaymentDto, long>, ISupplierPaymentService
{
    private readonly SGenDbContext _context;
    private readonly IMapper _mapper;
    private readonly IIdentityServices _identityService;
    public SupplierPaymentService(IQueryRepository<SupplierPayment, long> repository,
        SGenDbContext context,
        IMapper mapper,
        IIdentityServices identityService,
        ICommandRepository<SupplierPayment, long> commandRepo)
    : base(repository, mapper, commandRepo)
    {
        _context = context;
        _mapper = mapper;
        _identityService = identityService;
    }

    public override async Task<SupplierPaymentDto> CreateAsync(SupplierPaymentDto objModel)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var identity = _identityService.GetIdentity();
            identity.TenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            identity.CompanyId = Guid.Parse("22222222-2222-2222-2222-222222222222");

            //Find Po
            var getPo = await _context.PurchaseOrders
                .FirstOrDefaultAsync(x => x.TenantId == identity.TenantId &&
                                    x.CompanyId == identity.CompanyId
                                    && x.PoId == objModel.PoId && x.IsActive == true);

            // Generate next Payment Id
            var paymentId = (await _context.SupplierPayments
                .Where(x => x.TenantId == identity.TenantId && x.CompanyId == identity.CompanyId)
                .Select(x => x.PaymentId)
                .ToListAsync())
                .DefaultIfEmpty(0)
                .Max() + 1;

            // 🔹 Master (SupplierPayment)
            var obj = new SupplierPayment
            {
                TenantId = identity.TenantId,
                CompanyId = identity.CompanyId,
                PaymentId = paymentId,
                PoId = objModel.PoId,
                SupplierId = objModel.SupplierId,
                PaymentDate = objModel.PaymentDate ?? DateTime.UtcNow,
                Cash = objModel.Cash,
                Card = objModel.Card,
                Cheque = objModel.Cheque,
                Upi = objModel.Upi,
                Neft = objModel.Neft,
                PayAmount = objModel.PayAmount,
                DeuAmount = (getPo.NetAmount - objModel.PayAmount),
                TotalAmount = getPo.NetAmount,
                Remarks = objModel.Remarks,
                PaymentMode = objModel.PaymentMode,
                IsActive = true,
                CreatedBy = objModel.CreatedBy ?? "admin",
                CreatedOn = DateTime.UtcNow
            };

            _context.SupplierPayments.Add(obj);

            var totalPaid = await _context.SupplierPayments
            .Where(x => x.TenantId == identity.TenantId && x.CompanyId == identity.CompanyId 
                                            && x.PoId == objModel.PoId && x.IsActive == true)
                                            .SumAsync(x => x.PayAmount);

            getPo.DueAmount = getPo.NetAmount -  + objModel.PayAmount;

            // debit ledger Id DR (Account)
            var po = await _context.PurchaseOrders.Include(x => x.SupplierMasters)
                                                 .FirstOrDefaultAsync(x => x.TenantId == identity.TenantId &&
                                                 x.CompanyId == identity.CompanyId && x.PoId == objModel.PoId && x.IsActive == true);

            string? supplierName = po?.SupplierMasters?.SupplierName;

            var getLedgers = await _context.LedgerMasters.Where(x => x.TenantId == identity.TenantId && x.CompanyId == identity.CompanyId 
                                                        && (x.LedgerName == objModel.PaymentMode || x.LedgerName == supplierName)).ToListAsync();

            // debit ledger Id DR (Account)
            //int? subledgerDetailId = getLedgerDetailId?.SubLedgerId;

            var poLedgerId = getLedgers.Where(x => x.LedgerName == supplierName)
                                           .Select(x => (int?)x.LedgerId).FirstOrDefault();

            // credit ledger Id CR (Account)
            var paymentLedgerId = getLedgers.Where(x => x.LedgerName == objModel.PaymentMode)
                                                .Select(x => (int?)x.LedgerId).FirstOrDefault();

            // credit ledger Id CR (Account)
           // int? subLedgerId = getSubLedgerId?.LedgerId;

            var ledgerDetailId = (await _context.LedgerDetails
            .Where(x => x.TenantId == identity.TenantId && x.CompanyId == identity.CompanyId)
            .Select(x => x.Id)
            .ToListAsync())
            .DefaultIfEmpty(0)
            .Max() + 1;

            // debit amount DR
            var objLedger = new LedgerDetail
            {
                TenantId = identity.TenantId,
                CompanyId = identity.CompanyId,
                Id = ledgerDetailId++,
                LedgerId = poLedgerId,
                //SubLedgerId = poSubLedgerId,
                EntryType = "Purchase",
                RefId = obj.PoId,
                AccountType = "DR",
                DebitAmount = (decimal)obj.PayAmount,
                CreditAmount = 0,
                EntryDate = obj.PaymentDate.Value.Date,
                Narration = $"Purchase {obj.PoId}",
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
                LedgerId = paymentLedgerId,
                //SubLedgerId = paymentSubLedgerId,
                EntryType = "Purchase",
                RefId = obj.PoId,
                AccountType = "CR",
                DebitAmount = 0,
                CreditAmount = (decimal)obj.PayAmount,
                EntryDate = obj.PaymentDate.Value.Date,
                Narration = $"Purchase {obj.PoId}",
                IsActive = true,
                CreatedBy = objModel.CreatedBy ?? "admin",
                CreatedOn = DateTime.UtcNow
            };

            _context.LedgerDetails.Add(ledger);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            var result = _mapper.Map<SupplierPaymentDto>(obj);
            return result;
        }
        catch(Exception ex) 
        {
            await transaction.RollbackAsync();
            throw new Exception($"Error while creating sale invoice: {ex.Message}", ex);
        }
    } 
}
