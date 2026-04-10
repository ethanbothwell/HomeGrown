namespace HomeGrown.Core.Application.DTOs.Auth;

public record AuthResponse(
    string AccessToken,
    string RefreshToken,
    DateTime AccessTokenExpiry,
    UserDto User
);

public record UserDto(
    Guid Id,
    string Name,
    string Email,
    string Role,
    string? ProfileImageUrl
);
