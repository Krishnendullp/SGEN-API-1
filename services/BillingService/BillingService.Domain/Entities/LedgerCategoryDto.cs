using System;
using SGen.Framework.Entities;

namespace BillingService.Domain.Entities
{
    public class LedgerCategoryDto
    {
        public Guid TenantId { get; set; }
        public Guid CompanyId { get; set; }
        public int Id { get; set; }

        public string CategoryName { get; set; }
        public string CategoryCode { get; set; }
        public int SortOrder { get; set; } 
        public bool IsActive { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public ICollection<LedgerMaster>? LedgerMasters { get; set; }
    }
}
