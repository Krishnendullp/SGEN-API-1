using System;
using Framework.Entities;

namespace SGen.Framework.Entities
{
    public partial class LedgerMaster : BaseEntity<long>
    {
        public Guid TenantId { get; set; }

        public Guid CompanyId { get; set; }

        public int LedgerId { get; set; }

        public int? LedgerCateId { get; set; }

        public int? ParentId { get; set; }

        public string? LedgerName { get; set; }

        public bool IsControlLedger { get; set; }

        public decimal OpeningBalance { get; set; }

        public string? OpeningType { get; set; }

        public decimal DebitAmount { get; set; }

        public decimal CreditAmount { get; set; }

        public string? GstNo { get; set; }

        public string? Mobile { get; set; }

        public string? Address { get; set; }

        //public bool IsActive { get; set; }

        public string? CreatedBy { get; set; }
        //public DateTime CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        //public DateTime? UpdatedOn { get; set; }

        // Navigation Property
        public LedgerCategory? LedgerCategory { get; set; } = null;
        public ICollection<SubLedger>? SubLedgers { get; set; }= new List<SubLedger>();
        public ICollection<SupplierMaster> SupplierMasters { get; set; } = new List<SupplierMaster>();
        public ICollection<CustomerMaster> CustomerMasters { get; set; } = new List<CustomerMaster>();
        public ICollection<Tax>? Taxes { get; set; } = new List<Tax>();
    }
}
