using Framework.Entities;
using SGen.Framework.Entities;
using System;

namespace BillingService.Domain.Entities
{
    public class SaleMasterDetailDto
    {
        public Guid TenantId { get; set; }

        public Guid CompanyId { get; set; }

        public int Id { get; set; }

        public int SaleId { get; set; }

        public int ItemId { get; set; }

        public decimal Quantity { get; set; } = 1.00M;

        public int TaxGroupId { get; set; }

        public decimal TaxableAmount { get; set; } = 0.00M;

        public decimal TotalTax { get; set; } = 0.00M;

        public decimal Total { get; set; } = 0.00M;

        public string? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; } 

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public bool IsActive { get; set; } = true;

        public decimal Rate { get; set; }

        // 🔗 Navigation Properties

        public virtual TaxGroupDto? TaxGroups { get; set; } = null;

        public virtual SaleMasterDto? SaleMasters { get; set; } = null;

        public virtual SaleItemMasterDto? SaleItemMasters { get; set; } = null;

        public virtual ICollection<SaleDetailTaxDto>? SaleDetailTaxs { get; set; }
    }
}
