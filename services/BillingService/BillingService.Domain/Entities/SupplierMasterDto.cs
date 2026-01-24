
using SGen.Framework.Entities;

namespace BillingService.Domain.Entities
{
    public class SupplierMasterDto
    {
        public Guid TenantId { get; set; }

        public Guid CompanyId { get; set; }

        public int SupplierId { get; set; }

        public string? SupplierCode { get; set; }

        public string SupplierName { get; set; } = null!;

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

        //[JsonIgnore]
        //public virtual ICollection<PurchaseOrderDto> PurchaseOrders { get; set; } = new List<PurchaseOrderDto>();

        public LedgerMaster? LedgerMasters { get; set; } = null;
    }
}
