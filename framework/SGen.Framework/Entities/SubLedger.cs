using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Entities;

namespace SGen.Framework.Entities
{
    public partial class SubLedger : BaseEntity<long>
    {
        public Guid TenantId { get; set; }
        public Guid CompanyId { get; set; }
        public int SubLedgerId { get; set; }

        public int? LedgerId { get; set; }

        public string? SubLedgerName { get; set; }
        public string? Mobile { get; set; }
        public string? Address { get; set; }
        public string? GstNo { get; set; }

        //public bool IsActive { get; set; }

        public string? CreatedBy { get; set; }
        //public DateTime CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        //public DateTime? UpdatedOn { get; set; }

        // Navigation
        public LedgerMaster? LedgerMaster { get; set; } = null;
    }
}
