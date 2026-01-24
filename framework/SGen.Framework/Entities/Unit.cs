using System;
using System.Collections.Generic;

namespace Framework.Entities;

public partial class Unit : BaseEntity<long>
{
    public Guid TenantId { get; set; }

    public Guid CompanyId { get; set; }

    public int UnitId { get; set; }

    public string? UnitName { get; set; }

    public string? Description { get; set; }

    public decimal? ConversionValue { get; set; }

    //public bool? IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    //public DateTime CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    //public DateTime? UpdatedOn { get; set; }

    public virtual ICollection<ItemMaster> ItemMasterrs { get; set; } = new List<ItemMaster>();

    public virtual ICollection<StoreTransaction> StoreTransactions { get; set; } = new List<StoreTransaction>();
}
