using System.Text.Json.Serialization;
namespace BillingService.Domain.Entities;

public class ItemUnitMappingDto
{
    public Guid TenantId { get; set; }

    public Guid CompanyId { get; set; }

    public int UnitMappingId { get; set; }

    public int? UnitId { get; set; }

    public int? ItemId { get; set; }

    public bool? IsDefault { get; set; }

    public bool? IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    [JsonIgnore]
    public virtual ICollection<ItemMasterDto> ItemMasters { get; set; } = new List<ItemMasterDto>();

    public virtual UnitDto? Units { get; set; }
}
