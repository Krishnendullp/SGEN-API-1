using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Entities;

namespace SGen.Framework.Entities
{
    public partial class SaleMasterTax
    {
        public Guid TenantId { get; set; }

        public Guid CompanyId { get; set; }

        public int Id { get; set; }

        public int SaleId { get; set; }

        public decimal? TaxPercentage { get; set; }

        public decimal? TaxAmount { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public virtual SaleMaster? SaleMasters { get; set; } = null;
    }
}
