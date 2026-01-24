using System;
using System.Collections.Generic;

namespace Framework.Entities;

public partial class TaxGroupSgstDetail : BaseEntity<long>
{
    //public Guid TenantId { get; set; }

    //public Guid CompanyId { get; set; }

    public int Id { get; set; }

    public int TaxGroupId { get; set; }

    public int? TaxId { get; set; }

    public int? SerialNo { get; set; }

    public decimal Rate { get; set; }

    //public bool? IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    //public DateTime CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    //public DateTime? UpdatedOn { get; set; }

    public virtual Tax? Taxs { get; set; }

    public virtual TaxGroup Taxgroups { get; set; } = null!;
}
