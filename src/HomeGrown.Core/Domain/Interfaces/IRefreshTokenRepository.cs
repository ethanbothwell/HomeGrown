using HomeGrown.Core.Domain.Entities;

namespace HomeGrown.Core.Domain.Interfaces;

public interface IRefreshTokenRepository : IRepository<RefreshToken>
{
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task RevokeAllForUserAsync(Guid userId);
}
