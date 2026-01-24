using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SGen.Framework.Entities;

namespace BillingService.Domain.Entities
{
    public class TaxDto
    {
        public Guid TenantId { get; set; }

        public Guid CompanyId { get; set; }

        public int TaxId { get; set; }

        public int LedgerId { get; set; }

        public int TaxTypeId { get; set; }

        public string? Name { get; set; }

        public decimal? Rate { get; set; }

        public bool? IsActive { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UdatedOn { get; set; }

        //[JsonIgnore]
        //public virtual ICollection<StoreTransactionTaxDto> StoreTransactionTaxes { get; set; } = new List<StoreTransactionTaxDto>();
        [JsonIgnore]
        public virtual ICollection<TaxGroupIgstDetailDto> TaxGroupIgstDetails { get; set; } = new List<TaxGroupIgstDetailDto>();
        [JsonIgnore]
        public virtual ICollection<TaxGroupSgstDetailDto> TaxGroupSgstDetails { get; set; } = new List<TaxGroupSgstDetailDto>();

        public LedgerMasterDto? LedgerMaster { get; set; } = null;

        /* public virtual TaxType TaxTypes { get; set; } = null!*/
    }
}
