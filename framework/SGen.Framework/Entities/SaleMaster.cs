using Framework.Entities;
using System;

namespace SGen.Framework.Entities
{
    public partial class SaleMaster : BaseEntity<long>
    {
        //public Guid TenantId { get; set; }

        //public Guid CompanyId { get; set; }

        public int Id { get; set; }

        public decimal TaxableAmount { get; set; } = 0.00M;

        public decimal TotalTax { get; set; } = 0.00M;

        public decimal TotalAmount { get; set; } = 0.00M;

        public decimal DiscountAmount { get; set; } = 0.00M;

        public DateTime SaleDate { get; set; }

        public int CustomerId { get; set; }

        public string? CreatedBy { get; set; } = null;

        //public DateTime? CreatedOn { get; set; } = DateTime.Now;

        public string? UpdatedBy { get; set; }

        //public DateTime? UpdatedOn { get; set; }

        //public bool IsActive { get; set; } = true;

        public int ProjectId { get; set; }

        public string? InvNo { get; set; }

        public string? FinYr { get; set; }

        public decimal? PaymentReceiveAmount { get; set; }

        public string? Discount { get; set; } = null;

        public string? CustomerNote { get; set; } = null;

        public decimal ItemTax { get; set; }

        public decimal NetAmount { get; set; }

        public int TaxGroupId { get; set; }

        public string? Status { get; set; } = null;


        // Optional navigation properties
        public virtual CustomerMaster? CustomerMasters { get; set; } = null;

        public virtual TaxGroup? TaxGroups { get; set; } = null;

        public virtual ICollection<SaleMasterDetail> SaleMasterDetails { get; set; } = new List<SaleMasterDetail>();

        public virtual ProjectMaster ProjectMasters { get; set; } = null ;

        public virtual ICollection<SaleMasterTax> SaleMasterTaxs { get; set; } = new List<SaleMasterTax>();

        public virtual ICollection<SaleInvoicePayment> SaleInvoicePayments { get; set; } = new List<SaleInvoicePayment>();

    }
}
