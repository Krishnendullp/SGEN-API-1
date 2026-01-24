using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Entities;

namespace SGen.Framework.Entities
{
    public class LedgerDetail : BaseEntity<long>
    {
        //public Guid TenantId { get; set; }

        //public Guid CompanyId { get; set; }

        public int Id { get; set; }

        public int? LedgerId { get; set; }

        public int? SubLedgerId { get; set; }

        public string EntryType { get; set; } = null!;

        public long? RefId { get; set; }

        public string? AccountType { get; set; }

        public decimal DebitAmount { get; set; }

        public decimal CreditAmount { get; set; }

        public DateTime EntryDate { get; set; }

        public string? Narration { get; set; }

        //public bool IsActive { get; set; }

        public string? CreatedBy { get; set; }

        //public DateTime CreatedOn { get; set; }

        public string? UpdatedBy { get; set; }

        //public DateTime? UpdatedOn { get; set; }

        // 🔗 Navigation Properties
        public LedgerMaster? Ledger { get; set; } = null;

        public SubLedger? SubLedger { get; set; } = null;
    }
}
