using System.Text.Json.Serialization;

namespace BillingService.Domain.Entities;

public class StoreTransactionDto
{
    public Guid TenantId { get; set; }

    public Guid CompanyId { get; set; }

    public int StoreId { get; set; }

    public int PoId { get; set; }

    public int ItemId { get; set; }

    public int TaxGroupId { get; set; }

    public string? TransactionType { get; set; }

    public DateTime? TransactionDate { get; set; }

    public int? Qty { get; set; }

    public decimal? NetAmount { get; set; }

    public decimal? Rate { get; set; }

    public string? Remarks { get; set; }

    public bool? IsActive { get; set; }

    public string? CreatedBy { get; set; } 

    public DateTime CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public decimal? TotalTaxableAmount { get; set; }

    public decimal? TotalTax { get; set; }

    public int UnitId { get; set; }

    public virtual ItemMasterDto? ItemMaster { get; set; }

    [JsonIgnore]
    public virtual PurchaseOrderDto? PurchaseOrder { get; set; }

    public virtual ICollection<StoreTransactionTaxDto> StoreTransactionTaxes { get; set; } = new List<StoreTransactionTaxDto>();

    public virtual TaxGroupDto? TaxGroup { get; set; }
}
