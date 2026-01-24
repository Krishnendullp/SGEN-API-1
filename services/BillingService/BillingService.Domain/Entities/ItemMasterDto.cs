using Framework.Entities;

namespace BillingService.Domain.Entities
{
    public class ItemMasterDto : BaseEntity<long>
    {
        public Guid? TenantId { get; set; }

        public Guid? CompanyId { get; set; }

        public int ItemId { get; set; }

        public int? UnitId { get; set; }

        public int? CategoryId { get; set; }

        public int? TaxGroupId { get; set; }

        public string? Name { get; set; }

        public string? HsnCode { get; set; }

        public decimal? Rate { get; set; }

        public decimal? GstPercent { get; set; }

        public bool? IsActive { get; set; }

        public string? CreatedBy { get; set; } 

        public DateTime CreatedOn { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
        
        public virtual UnitDto? Units { get; set; }

        //public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; } = new List<PurchaseOrderDetail>();

        //public virtual ICollection<StoreTransaction> StoreTransactions { get; set; } = new List<StoreTransaction>();

        public virtual TaxGroupDto? TaxGroups { get; set; }

    }
}
