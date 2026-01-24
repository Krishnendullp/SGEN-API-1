using System;
using System.Text.Json.Serialization;

namespace Framework.Entities;

public partial class StoreTransactionTax
{
    public Guid TenantId { get; set; }

    public Guid CompanyId { get; set; }

    public int StoreTaxId { get; set; }

    public int StoreId { get; set; }

    public int TaxId { get; set; }

    public decimal? TaxPercentage { get; set; }

    public decimal? TaxAmount { get; set; }

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
    [JsonIgnore]
    public virtual StoreTransaction StoreTransaction { get; set; } = null!;

    public virtual Tax Taxs { get; set; } = null!;

}
