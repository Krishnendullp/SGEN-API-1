using Framework.Entities;

namespace BillingService.Domain.Entities
{
    public class SupplierReturnDto
    {
        public Guid TenantId { get; set; }

        public Guid CompanyId { get; set; }

        public int ReturnId { get; set; }

        public int? ProjectId { get; set; }

        public int SupplierId { get; set; }

        public string? ReturnNo { get; set; } 

        public DateTime ReturnDate { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal TotalDiscount { get; set; }

        public decimal TotalTax { get; set; }

        public decimal NetAmount { get; set; }

        public decimal? RoundOff { get; set; }

        public string? Status { get; set; } 

        public string? Remarks { get; set; }

        public bool IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public virtual ProjectMasterDto? ProjectMasters { get; set; } = null;

        public virtual SupplierMasterDto? SupplierMasters { get; set; }

        public virtual ICollection<SupplierReturnDetailDto> SupplierReturnDetails { get; set; } = new List<SupplierReturnDetailDto>();

    }
}
