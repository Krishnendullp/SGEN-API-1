using BillingService.Domain.Entities;
using BillingService.Domain.Repositories;

namespace BillingService.Infrastructure.Repositories;

public class InMemoryInvoiceRepository : IInvoiceRepository
{
    private readonly List<Invoice> _invoices = new();

    public void Add(Invoice invoice) => _invoices.Add(invoice);

    public IEnumerable<Invoice> GetAll() => _invoices;
}
