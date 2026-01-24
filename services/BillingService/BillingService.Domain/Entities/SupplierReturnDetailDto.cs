using Framework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingService.Domain.Entities
{
    public class SupplierReturnDetailDto
    {
        public Guid TenantId { get; set; }

        public Guid CompanyId { get; set; }

        public int ReturnDetailId { get; set; }

        public int ReturnId { get; set; }

        public int PoId { get; set; }

        public int ItemId { get; set; }

        public decimal Qty { get; set; }

        public decimal Price { get; set; }

        public decimal? Total { get; set; }

        public string? Remarks { get; set; }

        public bool IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public virtual ItemMasterDto? ItemMasters { get; set; }

        public virtual PurchaseOrderDto? PurchaseOrders { get; set; }

        public virtual SupplierReturnDto? SupplierReturns { get; set; } 
    }
}
