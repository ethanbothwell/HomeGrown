namespace HomeGrown.Core.Domain.Entities;

public class Farm
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid OwnerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public string? Philosophy { get; set; }
    public string? Location { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string? ImageUrl { get; set; }
    public double Rating { get; set; }
    public int ReviewCount { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public User Owner { get; set; } = null!;
    public ICollection<Product> Products { get; set; } = [];
    public ICollection<Review> Reviews { get; set; } = [];
    public ICollection<FarmPractice> Practices { get; set; } = [];
}
