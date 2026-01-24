using BillingService.Application.Interfaces;
using BillingService.Domain.Entities;
using BillingService.Domain.Repositories;

namespace BillingService.Application.Services;

public class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository _repo;

    public InvoiceService(IInvoiceRepository repo) => _repo = repo;

    public Invoice GenerateInvoice(Guid customerId, decimal amount)
    {
        var invoice = new Invoice { CustomerId = customerId, Amount = amount };
        _repo.Add(invoice);
        return invoice;
    }
}
