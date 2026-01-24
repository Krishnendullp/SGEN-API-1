using System;
using Framework.Entities;
using SGen.Framework.Entities;

namespace BillingService.Domain.Entities
{
    public class LedgerMasterDto
    {
        public Guid TenantId { get; set; }

        public Guid CompanyId { get; set; }

        public int LedgerId { get; set; }

        public int? LedgerCateId { get; set; }

        public int? ParentId { get; set; }

        public string? LedgerName { get; set; }

        public bool IsControlLedger { get; set; }

        public decimal OpeningBalance { get; set; }

        public string? OpeningType {  get; set; }

        public decimal DebitAmount { get; set; }

        public decimal CreditAmount { get; set; }

        public string? GstNo { get; set; }

        public string? Mobile { get; set; }

        public string? Address { get; set; }

        public bool IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        // Navigation Property
        public LedgerCategoryDto? LedgerCategory { get; set; }
        public ICollection<SubLedgerDto>? SubLedgers { get; set; } = new List<SubLedgerDto>();
        public ICollection<SupplierMasterDto> Suppliers { get; set; } = new List<SupplierMasterDto>();
        public ICollection<TaxDto>? Taxes { get; set; } = new List<TaxDto>();

    }
}
