using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGen.Framework.Entities;

namespace BillingService.Domain.Entities
{
    public class SaleMasterTaxDto
    {
        public Guid TenantId { get; set; }

        public Guid CompanyId { get; set; }

        public int Id { get; set; }

        public int SaleId { get; set; }

        public decimal? TaxPercentage { get; set; }

        public decimal? TaxAmount { get; set; }

        public bool IsActive { get; set; }

        public string? CreatedBy { get; set; } 

        public DateTime CreatedOn { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public virtual SaleMasterDto? SaleMasters { get; set; } = null;

    }
}
