using System.Text.Json.Serialization;
using SGen.Framework.Entities;

namespace BillingService.Domain.Entities;

public partial class SaleInvoicePaymentDto
{
    public Guid TenantId { get; set; }

    public Guid CompanyId { get; set; }

    public long InvoicePaymentId { get; set; }

    public int SaleId { get; set; }

    public string? Remarks { get; set; }

    public bool IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public decimal? PayAmount { get; set; }

    public DateTime? PayDate { get; set; }

    public string? PaymentMode { get; set; }

    /// <summary>
    /// [JsonIgnore]
    /// </summary>
    public virtual SaleMasterDto? SaleMasters { get; set; } = null;
}
