using System.Text.Json.Serialization;


namespace BillingService.Domain.Entities
{
    public class StoreTransactionTaxDto
    {
        public Guid TenantId { get; set; }

        public Guid CompanyId { get; set; }

        public int StoreTaxId { get; set; }

        public int StoreId { get; set; }

        public int TaxId { get; set; }

        public decimal? TaxPercentage { get; set; }

        public decimal? TaxAmount { get; set; }

        public bool IsActive { get; set; }

        public string? CreatedBy { get; set; } 

        public DateTime CreatedOn { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        [JsonIgnore]
        public virtual StoreTransactionDto? StoreTransaction { get; set; }

        public virtual TaxDto? Taxs { get; set; }
    }
}
