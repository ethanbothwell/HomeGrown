using HomeGrown.Core.Domain.Enums;

namespace HomeGrown.Core.Domain.Entities;

public class Order
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid BuyerId { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public decimal Total { get; set; }
    public string? ShippingAddress { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public User Buyer { get; set; } = null!;
    public ICollection<OrderItem> Items { get; set; } = [];
}
