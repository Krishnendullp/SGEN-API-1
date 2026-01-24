using System;
namespace Framework.Entities;
public partial class PurchaseOrder : BaseEntity<long>
{
    public Guid TenantId { get; set; }

    public Guid CompanyId { get; set; }

    public int PoId { get; set; }

    public int ProjectId { get; set; }

    public int SupplierId { get; set; }

    public string? PoNo { get; set; }

    public DateTime? PoDate { get; set; }

    public decimal? TotalTaxableAmount { get; set; }

    public decimal? TotalDiscount { get; set; }

    public decimal? TotalTax { get; set; }

    public decimal? NetAmount { get; set; }

    public decimal? RoundOff { get; set; }

    public string? Status { get; set; }

    public string? Remarks { get; set; }

    public bool? IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? FinYear { get; set; }

    public decimal? DueAmount { get; set; }

    public virtual ProjectMaster ProjectMasters { get; set; } = null!;

    public virtual ICollection<StoreTransaction> StoreTransactions { get; set; } = new List<StoreTransaction>();

    public virtual SupplierMaster SupplierMasters { get; set; } = null!;

    public virtual ICollection<SupplierPayment> SupplierPayments { get; set; } = new List<SupplierPayment>();

    public virtual ICollection<SupplierReturnDetail> SupplierReturnDetails { get; set; } = new List<SupplierReturnDetail>();
}
