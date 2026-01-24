using System;
using Framework.Entities;


namespace SGen.Framework.Entities
{
    public partial class LedgerCategory : BaseEntity<long>
    {
        public Guid TenantId { get; set; }
        public Guid CompanyId { get; set; }
        public int Id { get; set; }

        public string CategoryName { get; set; }
        public string CategoryCode { get; set; }
        public int SortOrder { get; set; } = 0;
        //public bool IsActive { get; set; } = true;

        public string CreatedBy { get; set; }
        //public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        //public DateTime? UpdatedOn { get; set; }

        public ICollection<LedgerMaster>? LedgerMasters { get; set; } = new List<LedgerMaster>();
    }
}
