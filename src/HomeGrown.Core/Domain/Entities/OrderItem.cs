namespace HomeGrown.Core.Domain.Entities;

public class OrderItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }  // price at time of purchase — never changes

    // Navigation
    public Order Order { get; set; } = null!;
    public Product Product { get; set; } = null!;
}
