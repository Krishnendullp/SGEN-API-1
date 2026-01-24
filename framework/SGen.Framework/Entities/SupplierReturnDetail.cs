using System;
using System.Collections.Generic;

namespace Framework.Entities;

public partial class SupplierReturnDetail 
{
    public Guid TenantId { get; set; }

    public Guid CompanyId { get; set; }

    public int ReturnDetailId { get; set; }

    public int ReturnId { get; set; }

    public int PoId { get; set; }

    public int ItemId { get; set; }

    public decimal Qty { get; set; }

    public decimal Price { get; set; }

    public decimal? Total { get; set; }

    public string? Remarks { get; set; }

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual ItemMaster ItemMasters { get; set; } = null!;

    public virtual PurchaseOrder PurchaseOrders { get; set; } = null!;

    public virtual SupplierReturn SupplierReturns { get; set; } = null!;
}
