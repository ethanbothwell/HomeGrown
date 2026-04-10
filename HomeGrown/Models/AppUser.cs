using System.ComponentModel.DataAnnotations;

namespace HomeGrown.Models;

public class AppUser
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = "";

    [Required, EmailAddress, MaxLength(256)]
    public string Email { get; set; } = "";

    [Required]
    public string PasswordHash { get; set; } = "";

    public bool IsFarmer { get; set; }
    public int? FarmId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
