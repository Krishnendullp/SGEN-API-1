

using Framework.Entities;
using SGen.Framework.Entities;

namespace BillingService.Domain.Entities
{
    public class ProjectMasterDto
    {
        public Guid TenantId { get; set; }

        public Guid CompanyId { get; set; }

        public int ProjectId { get; set; }

        public string? LoaNo { get; set; }

        public string? Name { get; set; }

        public decimal? Amount { get; set; }

        public string? Description { get; set; }

        //public bool? IsActive { get; set; }

        public string? CreatedBy { get; set; } 

        //public DateTime CreatedOn { get; set; }

        public string? UpdatedBy { get; set; }

        //public DateTime? UpdatedOn { get; set; }

        public virtual ICollection<PurchaseOrderDto> PurchaseOrders { get; set; } = new List<PurchaseOrderDto>();

        public virtual ICollection<SupplierReturnDto> SupplierReturns { get; set; } = new List<SupplierReturnDto>();

        public virtual ICollection<SaleMasterDto> SaleMasters { get; set; } = new List<SaleMasterDto>();
    }
}
