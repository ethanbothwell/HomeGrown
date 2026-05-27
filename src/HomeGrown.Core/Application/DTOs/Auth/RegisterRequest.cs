using System.ComponentModel.DataAnnotations;

namespace HomeGrown.Core.Application.DTOs.Auth;

public record RegisterRequest(
    [Required, MaxLength(100)] string Name,
    [Required, EmailAddress] string Email,
    [Required, MinLength(8)] string Password,
    string Role = "Buyer",     // "Buyer" | "Farmer"
    string? Community = null   // e.g. "Portland", "Corvallis", "Benton County"
);
