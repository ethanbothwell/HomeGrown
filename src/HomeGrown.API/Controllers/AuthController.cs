using HomeGrown.API.Services;
using HomeGrown.Core.Application.DTOs.Auth;
using HomeGrown.Core.Domain.Entities;
using HomeGrown.Core.Domain.Enums;
using HomeGrown.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HomeGrown.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IUnitOfWork uow, TokenService tokenService, IEmailService email) : ControllerBase
{
    /// <summary>POST /api/auth/register</summary>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (await uow.Users.EmailExistsAsync(request.Email))
            return Conflict(new { error = "An account with that email already exists." });

        if (!Enum.TryParse<UserRole>(request.Role, ignoreCase: true, out var role) ||
            role == UserRole.Admin)  // admins cannot self-register
            role = UserRole.Buyer;

        var user = new User
        {
            Email = request.Email.ToLower(),
            Name = request.Name,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = role
        };

        await uow.Users.AddAsync(user);
        await uow.SaveChangesAsync();

        _ = email.SendWelcomeAsync(user.Email, user.Name, user.Role.ToString());

        return Ok(await BuildAuthResponse(user));
    }

    /// <summary>POST /api/auth/login</summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await uow.Users.GetByEmailAsync(request.Email);

        if (user is null || user.PasswordHash is null ||
            !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return Unauthorized(new { error = "Invalid email or password." });

        return Ok(await BuildAuthResponse(user));
    }

    /// <summary>POST /api/auth/refresh</summary>
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
    {
        var storedToken = await uow.RefreshTokens.GetByTokenAsync(request.RefreshToken);

        if (storedToken is null || storedToken.IsRevoked || storedToken.ExpiresAt < DateTime.UtcNow)
            return Unauthorized(new { error = "Invalid or expired refresh token." });

        // Rotate: revoke the old token, issue a new pair
        storedToken.IsRevoked = true;
        return Ok(await BuildAuthResponse(storedToken.User));
    }

    /// <summary>POST /api/auth/logout</summary>
    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] RefreshRequest request)
    {
        var storedToken = await uow.RefreshTokens.GetByTokenAsync(request.RefreshToken);

        if (storedToken is not null)
            storedToken.IsRevoked = true;

        await uow.SaveChangesAsync();
        return NoContent();
    }

    // ─── Helpers ──────────────────────────────────────────────────────────────

    private async Task<AuthResponse> BuildAuthResponse(User user)
    {
        var accessToken = tokenService.GenerateAccessToken(user);
        var refreshTokenStr = tokenService.GenerateRefreshToken();

        var refreshToken = new RefreshToken
        {
            UserId = user.Id,
            Token = refreshTokenStr,
            ExpiresAt = tokenService.RefreshTokenExpiry
        };

        await uow.RefreshTokens.AddAsync(refreshToken);
        await uow.SaveChangesAsync();

        return new AuthResponse(
            AccessToken: accessToken,
            RefreshToken: refreshTokenStr,
            AccessTokenExpiry: tokenService.AccessTokenExpiry,
            User: new UserDto(user.Id, user.Name, user.Email, user.Role.ToString(), user.ProfileImageUrl)
        );
    }
}
