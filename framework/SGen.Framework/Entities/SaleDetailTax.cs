using Framework.Entities;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SGen.Framework.Entities
{
    public partial class SaleDetailTax
    {
        public Guid TenantId { get; set; }

        public Guid CompanyId { get; set; }

        public int Id { get; set; }

        public int SaleDetailId { get; set; }

        public int TaxId { get; set; }

        public decimal? TaxPercentage { get; set; }

        public decimal? TaxAmount { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    
        public virtual SaleMasterDetail SaleMasterDetails { get; set; } = null!;

        public virtual Tax Taxs { get; set; } = null!;
    }
}
