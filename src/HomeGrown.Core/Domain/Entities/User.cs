using HomeGrown.Core.Domain.Enums;

namespace HomeGrown.Core.Domain.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? PasswordHash { get; set; }   // null when user signs in via OAuth only
    public string? GoogleId { get; set; }
    public string? ProfileImageUrl { get; set; }
    public UserRole Role { get; set; } = UserRole.Buyer;
    public bool IsEmailVerified { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public Farm? Farm { get; set; }
    public ICollection<Order> Orders { get; set; } = [];
    public ICollection<Review> Reviews { get; set; } = [];
    public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
}
