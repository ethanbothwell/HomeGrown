using System.ComponentModel.DataAnnotations;

namespace HomeGrown.Core.Application.DTOs.Auth;

public record LoginRequest(
    [Required, EmailAddress] string Email,
    [Required] string Password
);
