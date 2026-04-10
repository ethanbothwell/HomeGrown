namespace HomeGrown.Core.Domain.Entities;

public class FarmPractice
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid FarmId { get; set; }
    public string Name { get; set; } = string.Empty;

    // Navigation
    public Farm Farm { get; set; } = null!;
}
