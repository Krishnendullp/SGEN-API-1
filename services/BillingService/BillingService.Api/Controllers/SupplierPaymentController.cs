using BillingService.Application.Interfaces;
using BillingService.Domain.Entities;
using Framework.Entities;
using Framework.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillingService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierPaymentController : BaseCrudController<SupplierPayment, SupplierPaymentDto, long>
    {
        public SupplierPaymentController(ISupplierPaymentService service) : base(service)
        {
        }
    }
}
