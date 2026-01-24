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
    public class SaleInvoicePaymentController : BaseCrudController<SaleInvoicePayment, SaleInvoicePaymentDto, long>
    {
        public SaleInvoicePaymentController(ISaleInvoicePaymentService service) : base(service)
        {
        }
    }
}
