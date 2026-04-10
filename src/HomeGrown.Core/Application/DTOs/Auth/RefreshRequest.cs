using System.ComponentModel.DataAnnotations;

namespace HomeGrown.Core.Application.DTOs.Auth;

public record RefreshRequest([Required] string RefreshToken);
