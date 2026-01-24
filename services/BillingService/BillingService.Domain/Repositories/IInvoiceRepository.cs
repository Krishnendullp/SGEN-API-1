using BillingService.Domain.Entities;

namespace BillingService.Domain.Repositories;

public interface IInvoiceRepository
{
    void Add(Invoice invoice);
    IEnumerable<Invoice> GetAll();
}
