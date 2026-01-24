using SGen.Framework.Entities;
using System;
using System.Text.Json.Serialization;

namespace BillingService.Domain.Entities
{
    public class TaxGroupDto
    {
        public Guid TenantId { get; set; }

        public Guid CompanyId { get; set; }

        public int TaxGroupId { get; set; }

        public string? Code { get; set; }

        public string? Description { get; set; }

        public bool? IsActive { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        [JsonIgnore]
        public virtual ICollection<ItemMasterDto> ItemMasters { get; set; } = new List<ItemMasterDto>();

        //public virtual ICollection<PurchaseOrderDetail> purchaseOrderDetails { get; set; } = new List<PurchaseOrderDetail>();

        //public virtual ICollection<StoreTransactionDto> StoreTransactions { get; set; } = new List<StoreTransactionDto>();
        
        public virtual ICollection<TaxGroupIgstDetailDto> TaxGroupIgstDetails { get; set; } = new List<TaxGroupIgstDetailDto>();
        
        public virtual ICollection<TaxGroupSgstDetailDto> TaxGroupSgstDetails { get; set; } = new List<TaxGroupSgstDetailDto>();

        public virtual ICollection<SaleMasterDetailDto> SaleMasterDetails { get; set; } = new List<SaleMasterDetailDto>();

    }
}
