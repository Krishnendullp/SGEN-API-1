using SGen.Framework.Entities;
using System;
using System.Text.Json.Serialization;

namespace Framework.Entities;

public partial class Tax : BaseEntity<long>
{
    public Guid TenantId { get; set; }

    public Guid CompanyId { get; set; }

    public int TaxId { get; set; }

    public int LedgerId { get; set; }

    public string? Name { get; set; }

    //public bool? IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    //public DateTime CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    //public DateTime? UdatedOn { get; set; }
    [JsonIgnore]
    public virtual ICollection<StoreTransactionTax> StoreTransactionTaxes { get; set; } = new List<StoreTransactionTax>();

    public virtual ICollection<TaxGroupIgstDetail> TaxGroupIgstDetails { get; set; } = new List<TaxGroupIgstDetail>();

    public virtual ICollection<TaxGroupSgstDetail> TaxGroupSgstDetails { get; set; } = new List<TaxGroupSgstDetail>();

    public LedgerMaster? LedgerMaster { get; set; }

    public virtual ICollection<SaleDetailTax> SaleMasterTaxs { get; set; } = new List<SaleDetailTax>();


}
