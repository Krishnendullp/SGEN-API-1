using System;
using System.Collections.Generic;
using SGen.Framework.Entities;

namespace BillingService.Domain.Entities
{
    public class LedgerDetailDto
    {
        public Guid TenantId { get; set; }

        public Guid CompanyId { get; set; }

        public int Id { get; set; }

        public int? LedgerId { get; set; }

        public int? SubLedgerId { get; set; }

        public string? EntryType { get; set; }

        public long? RefId { get; set; }

        public string? AccountType { get; set; }

        public decimal DebitAmount { get; set; }

        public decimal CreditAmount { get; set; }

        public DateTime EntryDate { get; set; }

        public string? Narration { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string? UpdatedBy { get; set; }

        // 🔗 Navigation Properties
        public LedgerMasterDto? Ledger { get; set; } 

        public SubLedgerDto? SubLedger { get; set; } 
    }
}
