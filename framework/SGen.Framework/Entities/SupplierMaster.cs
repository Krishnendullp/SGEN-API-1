using System;
using System.Collections.Generic;
using SGen.Framework.Entities;

namespace Framework.Entities;

public partial class SupplierMaster : BaseEntity<long>
{
    public Guid TenantId { get; set; }

    public Guid CompanyId { get; set; }

    public int SupplierId { get; set; }

    public int? LedgerId { get; set; }

    public string? SupplierCode { get; set; }

    public string SupplierName { get; set; } = null!;

    public string? ContactPerson { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Pincode { get; set; }

    public string? Country { get; set; }

    public string? GstNumber { get; set; }

    //public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    //public DateTime CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    //public DateTime? UpdatedOn { get; set; }

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();

    public virtual ICollection<SupplierPayment> SupplierPayments { get; set; } = new List<SupplierPayment>();

    public virtual ICollection<SupplierReturn> SupplierReturns { get; set; } = new List<SupplierReturn>();

    public LedgerMaster? LedgerMasters { get; set; }
}
