using Microsoft.AspNetCore.Mvc;
using BillingService.Application.Interfaces;


namespace BillingService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoiceController : ControllerBase
{
    private readonly IInvoiceService _service;

    public InvoiceController(IInvoiceService service) => _service = service;

    [HttpPost("generate")]
    public IActionResult Generate([FromBody] decimal amount)
    {
        var invoice = _service.GenerateInvoice(Guid.NewGuid(), amount);
        return Ok(invoice);
    }
}
