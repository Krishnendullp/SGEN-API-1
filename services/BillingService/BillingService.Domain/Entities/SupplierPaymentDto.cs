using Framework.Entities;
using System;

namespace BillingService.Domain.Entities
{
    public class SupplierPaymentDto
    {
        public Guid TenantId { get; set; }

        public Guid CompanyId { get; set; }

        public int PaymentId { get; set; }

        public int? PoId { get; set; }

        public int? SupplierId { get; set; }

        public DateTime? PaymentDate { get; set; }

        public decimal? Cash { get; set; }

        public decimal? Card { get; set; }

        public decimal? Cheque { get; set; }

        public decimal? Upi { get; set; }

        public decimal? Neft { get; set; }

        public decimal? PayAmount { get; set; }

        public decimal? DeuAmount { get; set; }

        public decimal? TotalAmount { get; set; }

        public string? PaymentMode { get; set; }

        public string? ReferenceNo { get; set; }

        public string? Remarks { get; set; }

        public bool IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public virtual PurchaseOrderDto? PurchaseOrders { get; set; }

        public virtual SupplierMasterDto? SupplierMasters { get; set; }
    }
}
