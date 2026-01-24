using BillingService.Domain.Entities;

namespace BillingService.Application.Interfaces;

public interface IInvoiceService
{
    Invoice GenerateInvoice(Guid customerId, decimal amount);
}
