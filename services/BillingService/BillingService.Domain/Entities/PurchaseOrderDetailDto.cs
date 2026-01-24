using Framework.Entities;
using System;

namespace BillingService.Domain.Entities
{
    public class PurchaseOrderDetailDto
    {
        public Guid TenantId { get; set; }

        public Guid CompanyId { get; set; }

        public int PoDetailId { get; set; }

        public int? PoId { get; set; }

        public int? ItemId { get; set; }

        public int? TaxGroupId { get; set; }

        public int? Qty { get; set; }

        public decimal? Rate { get; set; }

        public decimal? GrossAmount { get; set; }

        public decimal? NetAmount { get; set; }

        public string? Remarks { get; set; }

        public bool? IsActive { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public virtual ItemMasterDto? ItemMasters { get; set; }

        public virtual PurchaseOrderDto? PurchaseOrders { get; set; }

        public virtual TaxGroupDto? TaxGroups { get; set; }
    }
}
