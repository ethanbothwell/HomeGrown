using HomeGrown.Core.Domain.Enums;

namespace HomeGrown.Core.Domain.Entities;

public class SubscriptionPlan
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid FarmId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public SubscriptionFrequency Frequency { get; set; }
    public int? MaxSubscribers { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public Farm Farm { get; set; } = null!;
    public ICollection<FarmSubscription> Subscriptions { get; set; } = [];
}
