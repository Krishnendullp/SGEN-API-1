using BillingService.Domain.Entities;
using Framework.Entities;
using Framework.Services;

namespace BillingService.Application.Interfaces
{
    public interface ISupplierPaymentService : ICrudService<SupplierPayment, SupplierPaymentDto, long>
    {
    }
}
