using Framework.Entities;
using SGen.Framework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingService.Domain.Entities
{
    public class SaleDetailTaxDto
    {
        public Guid TenantId { get; set; }

        public Guid CompanyId { get; set; }

        public int Id { get; set; }

        public int SaleDetailId { get; set; }

        public int TaxId { get; set; }

        public decimal? TaxPercentage { get; set; }

        public decimal? TaxAmount { get; set; }

        public bool IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public virtual SaleMasterDetailDto? SaleMasterDetails { get; set; } = null!;

        public virtual TaxDto? Taxs { get; set; } = null!;
    }
}
