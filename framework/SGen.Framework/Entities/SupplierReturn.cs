using System;
using System.Collections.Generic;

namespace Framework.Entities;

public partial class SupplierReturn : BaseEntity<long>
{
    public Guid TenantId { get; set; }

    public Guid CompanyId { get; set; }

    public int ReturnId { get; set; }

    public int? ProjectId { get; set; }

    public int SupplierId { get; set; }

    public string ReturnNo { get; set; } = null!;

    public DateTime ReturnDate { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal TotalDiscount { get; set; }

    public decimal TotalTax { get; set; }

    public decimal NetAmount { get; set; }

    public decimal? RoundOff { get; set; }

    public string Status { get; set; } = null!;

    public string? Remarks { get; set; }

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual ProjectMaster? ProjectMasters { get; set; }

    public virtual SupplierMaster SupplierMasters { get; set; } = null!;

    public virtual ICollection<SupplierReturnDetail> SupplierReturnDetails { get; set; } = new List<SupplierReturnDetail>();
}
