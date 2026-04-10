namespace HomeGrown.Core.Domain.Entities;

public class Review
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid FarmId { get; set; }
    public Guid UserId { get; set; }
    public int Rating { get; set; }   // 1–5
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public Farm Farm { get; set; } = null!;
    public User User { get; set; } = null!;
}
