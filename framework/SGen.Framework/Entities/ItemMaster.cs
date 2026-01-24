using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Entities;

public partial class ItemMaster : BaseEntity<long>
{
    public Guid TenantId { get; set; }

    public Guid CompanyId { get; set; }

    public int ItemId { get; set; }

    public int UnitId { get; set; }

    public int? CategoryId { get; set; }

    public int? TaxGroupId { get; set; }

    public string? Name { get; set; }

    public string? HsnCode { get; set; }

    public decimal? Rate { get; set; }

    public decimal? GstPercent { get; set; }
    
    //public bool? IsActive { get; set; }

    public string CreatedBy { get; set; }

    //public DateTime CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    //public DateTime? UpdatedOn { get; set; }

    public virtual ICollection<StoreTransaction>? StoreTransactions { get; set; }

    public virtual TaxGroup TaxGroups { get; set; } = null!;

    public virtual ICollection<SupplierReturnDetail> SupplierReturnDetails { get; set; } = new List<SupplierReturnDetail>();

    public virtual Unit Units { get; set; } = null!;
}
