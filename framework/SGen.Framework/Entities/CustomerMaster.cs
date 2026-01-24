using Framework.Entities;
using System;
using System.Collections.Generic;


namespace SGen.Framework.Entities
{
    public partial class CustomerMaster : BaseEntity<long>
    {
        //public Guid TenantId { get; set; }

        //public Guid CompanyId { get; set; }

        public int CustomerId { get; set; }

        public int? LedgerId {  get; set; }

        public string? CustomerCode { get; set; }

        public string CustomerName { get; set; } = null!;

        public string? ContactPerson { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Pincode { get; set; }

        public string? Country { get; set; }

        public string? GstNumber { get; set; }

        //public bool IsActive { get; set; }

        public string CreatedBy { get; set; } = null!;

        //public DateTime CreatedOn { get; set; }

        public string? UpdatedBy { get; set; }

        //public DateTime? UpdatedOn { get; set; }

        // 🔗 Navigation property (One Customer → Many Sales)
        public virtual ICollection<SaleMaster> SaleMasters { get; set; } = new List<SaleMaster>();

        public LedgerMaster? LedgerMasters { get; set; }
    }
}
