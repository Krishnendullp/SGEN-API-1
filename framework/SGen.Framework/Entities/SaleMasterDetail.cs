using Framework.Entities;
using System;

namespace SGen.Framework.Entities
{
    public partial class SaleMasterDetail
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

        public DateTime? CreatedOn { get; set; } = DateTime.Now;

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public bool IsActive { get; set; } = true;

        public decimal Rate { get; set; }

        // 🔗 Navigation Properties

        public virtual TaxGroup? TaxGroups { get; set; } = null;

        public virtual SaleMaster? SaleMasters { get; set; } = null;

        public virtual SaleItemMaster SaleItemMasters { get; set; } = null;

        public virtual ICollection<SaleDetailTax> SaleDetailTaxs { get; set; } = new List<SaleDetailTax>();

    }
}
