using System.Text.Json.Serialization;

namespace BillingService.Domain.Entities
{
    public class TaxGroupIgstDetailDto
    {
        public Guid TenantId { get; set; }

        public Guid CompanyId { get; set; }

        public int Id { get; set; }

        public int TaxGroupId { get; set; }

        public int? TaxId { get; set; }

        public int? SerialNo { get; set; }

        public decimal? Rate { get; set; }

        public bool? IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public virtual TaxDto? Taxs { get; set; }
        [JsonIgnore]
        public virtual TaxGroupDto? TaxGroups { get; set; }
    }
}
