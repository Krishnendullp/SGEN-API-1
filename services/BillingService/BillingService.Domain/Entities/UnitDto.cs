

using Framework.Entities;
using System.Text.Json.Serialization;

namespace BillingService.Domain.Entities
{
    public class UnitDto
    {
        public Guid? TenantId { get; set; }

        public Guid? CompanyId { get; set; }

        public int UnitId { get; set; }

        public string? UnitName { get; set; }

        public string? Description { get; set; }

        public decimal? ConversionValue { get; set; }

        public bool? IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        [JsonIgnore]
        public virtual ICollection<ItemMasterDto> ItemMasterrs { get; set; } = new List<ItemMasterDto>();

        public virtual ICollection<StoreTransactionDto> StoreTransactions { get; set; } = new List<StoreTransactionDto>();
    }
}
