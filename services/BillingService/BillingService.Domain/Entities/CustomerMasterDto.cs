using SGen.Framework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingService.Domain.Entities
{
    public class CustomerMasterDto
    {
        public Guid TenantId { get; set; }

        public Guid CompanyId { get; set; }

        public int CustomerId { get; set; }

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

        public bool IsActive { get; set; }

        public string? CreatedBy { get; set; } 

        public DateTime CreatedOn { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        // 🔗 Navigation property (One Customer → Many Sales)
        public virtual ICollection<SaleMasterDto> SaleMasters { get; set; } = new List<SaleMasterDto>();

        public LedgerMasterDto? LedgerMasters { get; set; }
    }
}
