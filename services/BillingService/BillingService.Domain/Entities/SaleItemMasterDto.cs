using Framework.Entities;
using SGen.Framework.Entities;
using System;

namespace BillingService.Domain.Entities
{
    public class SaleItemMasterDto
    {
        public Guid TenantId { get; set; }

        public Guid CompanyId { get; set; }

        public int Id { get; set; }

        public int TaxGroupId { get; set; }

        public string? Name { get; set; }

        public decimal Rate { get; set; } 

        public string? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public bool IsActive { get; set; } = true;

        // 🔗 Optional navigation (if you have TaxGroup table)
        public virtual TaxGroup? TaxGroups { get; set; }

        public virtual ICollection<SaleMasterDetail>? SaleMasterDetails { get; set; } 
    }
}
