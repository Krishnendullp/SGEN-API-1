using Framework.Entities;

namespace BillingService.Domain.Entities;

public class ItemModel : BaseEntity<long>
{
    public string Name { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}

