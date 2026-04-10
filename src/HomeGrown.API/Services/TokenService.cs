using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using HomeGrown.Core.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace HomeGrown.API.Services;

public class TokenService(IConfiguration configuration)
{
    private readonly string _secret = configuration["Jwt:Secret"]
        ?? throw new InvalidOperationException("Jwt:Secret is not configured.");
    private readonly string _issuer = configuration["Jwt:Issuer"] ?? "HomeGrown";
    private readonly string _audience = configuration["Jwt:Audience"] ?? "HomeGrownClients";

    /// <summary>
    /// Generates a short-lived JWT (15 minutes).
    /// The token carries the user's ID, email, and role — everything the API
    /// needs to authorize a request without hitting the database.
    /// </summary>
    public string GenerateAccessToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    /// <summary>
    /// Generates a cryptographically secure refresh token.
    /// Stored in the database — used to issue a new access token after expiry.
    /// </summary>
    public string GenerateRefreshToken()
    {
        var bytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(bytes);
        return Convert.ToBase64String(bytes);
    }

    public DateTime AccessTokenExpiry => DateTime.UtcNow.AddMinutes(15);
    public DateTime RefreshTokenExpiry => DateTime.UtcNow.AddDays(30);
}
