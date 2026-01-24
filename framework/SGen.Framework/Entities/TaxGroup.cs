using SGen.Framework.Entities;
using System;
using System.Collections.Generic;

namespace Framework.Entities;

public partial class TaxGroup : BaseEntity<long>
{
    //public Guid TenantId { get; set; }

    //public Guid CompanyId { get; set; }

    public int TaxGroupId { get; set; }

    public string? Code { get; set; }

    public string? Description { get; set; }

    //public bool? IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    //public DateTime CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    //public DateTime? UpdatedOn { get; set; }

    public virtual ICollection<ItemMaster> ItemMasters { get; set; } = new List<ItemMaster>();

    public virtual ICollection<StoreTransaction> StoreTransactions { get; set; } = new List<StoreTransaction>();

    public virtual ICollection<TaxGroupIgstDetail> TaxGroupIgstDetails { get; set; } = new List<TaxGroupIgstDetail>();

    public virtual ICollection<TaxGroupSgstDetail> TaxGroupSgstDetails { get; set; } = new List<TaxGroupSgstDetail>();

    public virtual ICollection<SaleItemMaster> SaleItemMasters { get; set; } = new List<SaleItemMaster>();

    public virtual ICollection<SaleMaster> SaleMasters { get; set; } = new List<SaleMaster>();

    public virtual ICollection<SaleMasterDetail> SaleMasterDetails { get; set; } = new List<SaleMasterDetail>();

}
