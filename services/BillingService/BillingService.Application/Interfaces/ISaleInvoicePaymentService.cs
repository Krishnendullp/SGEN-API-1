using BillingService.Domain.Entities;
using Framework.Entities;
using Framework.Services;
using System;


namespace BillingService.Application.Interfaces
{
    public interface ISaleInvoicePaymentService : ICrudService<SaleInvoicePayment, SaleInvoicePaymentDto, long>
    {
    }
}
