namespace HomeGrown.Core.Domain.Entities;

public class Product
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid FarmId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Category { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? Unit { get; set; }
    public string? ImageUrl { get; set; }
    public bool InStock { get; set; } = true;
    public string? Tags { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public Farm Farm { get; set; } = null!;
    public ICollection<OrderItem> OrderItems { get; set; } = [];
}
