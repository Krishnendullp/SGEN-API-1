using System;
using System.Collections.Generic;
using SGen.Framework.Entities;

namespace Framework.Entities;

public partial class ProjectMaster : BaseEntity<long>
{
   // public Guid TenantId { get; set; }

   // public Guid CompanyId { get; set; }

    public int ProjectId { get; set; }

    public string? LoaNo { get; set; }

    public string? Name { get; set; }

    public decimal? Amount { get; set; }

    public string? Description { get; set; }

    //public bool? IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    //public DateTime CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    //public DateTime? UpdatedOn { get; set; }

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();

    public virtual ICollection<SupplierReturn> SupplierReturns { get; set; } = new List<SupplierReturn>();

    public virtual ICollection<SaleMaster> SaleMasters { get; set; } = new List<SaleMaster>();
}
