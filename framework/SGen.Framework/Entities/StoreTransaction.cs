using System;
using System.Collections.Generic;

namespace Framework.Entities;

public partial class StoreTransaction
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

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public decimal? TotalTaxableAmount { get; set; }

    public decimal? TotalTax { get; set; }

    public int UnitId { get; set; }

    public virtual ItemMaster ItemMaster { get; set; } = null!;

    public virtual PurchaseOrder PurchaseOrder { get; set; } = null!;

    public virtual ICollection<StoreTransactionTax> StoreTransactionTaxes { get; set; } = new List<StoreTransactionTax>();

    public virtual TaxGroup TaxGroup { get; set; } = null!;

    public virtual Unit units { get; set; } = null!;
}
