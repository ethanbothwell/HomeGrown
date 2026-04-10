using System.ComponentModel.DataAnnotations;

namespace HomeGrown.Models;

public class Subscriber
{
    public int Id { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(256)]
    public string Email { get; set; } = "";

    public DateTime SubscribedAt { get; set; } = DateTime.UtcNow;
}
