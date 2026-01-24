using System;
using SGen.Framework.Entities;

namespace BillingService.Domain.Entities
{
    public class VoucherMasterDto
    {
        public Guid TenantId { get; set; }

        public Guid CompanyId { get; set; }

        public int VoucherId { get; set; }

        public string? VoucherNo { get; set; }

        public string? VoucherType { get; set; }

        public DateTime? VoucherDate { get; set; }

        public string? ReferenceNo { get; set; }

        public string? Narration { get; set; }

        public bool IsPosted { get; set; }

        public bool IsCancelled { get; set; }

        public bool IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public virtual ICollection<LedgerDetailDto>? LedgerDetails { get; set; }
    }
}
