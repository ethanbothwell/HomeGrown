namespace HomeGrown.Core.Application.DTOs.Auth;

public record AuthResponse(
    string AccessToken,
    string RefreshToken,
    DateTime AccessTokenExpiry,
    UserDto User,
    int? WaitlistPosition = null,
    int? Remaining = null,
    string? Community = null
);

public record UserDto(
    Guid Id,
    string Name,
    string Email,
    string Role,
    string? ProfileImageUrl
);
