using System;
using System.Collections.Generic;

namespace Framework.Entities;

public partial class SupplierPayment : BaseEntity<long>
{
    public Guid TenantId { get; set; }

    public Guid CompanyId { get; set; }

    public int PaymentId { get; set; }

    public int? PoId { get; set; }

    public int? SupplierId { get; set; }

    public DateTime? PaymentDate { get; set; }

    public decimal? Cash { get; set; }

    public decimal? Card { get; set; }

    public decimal? Cheque { get; set; }

    public decimal? Upi { get; set; }

    public decimal? Neft { get; set; }

    public decimal? PayAmount { get; set; }

    public decimal? DeuAmount { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? PaymentMode { get; set; }

    public string? ReferenceNo { get; set; }

    public string? Remarks { get; set; }

    //public bool IsActive { get; set; }

    //public string CreatedBy { get; set; } = null!;

    //public DateTime Created_on { get; set; }

    public string? UpdatedBy { get; set; }

    //public DateTime? UpdatedOn { get; set; }

    public virtual PurchaseOrder? PurchaseOrders { get; set; }

    public virtual SupplierMaster? SupplierMasters { get; set; }
}
