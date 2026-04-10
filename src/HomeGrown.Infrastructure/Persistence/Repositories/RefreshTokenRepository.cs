using HomeGrown.Core.Domain.Entities;
using HomeGrown.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeGrown.Infrastructure.Persistence.Repositories;

public class RefreshTokenRepository(AppDbContext context) : Repository<RefreshToken>(context), IRefreshTokenRepository
{
    public async Task<RefreshToken?> GetByTokenAsync(string token) =>
        await DbSet.Include(rt => rt.User).FirstOrDefaultAsync(rt => rt.Token == token);

    public async Task RevokeAllForUserAsync(Guid userId)
    {
        var tokens = await DbSet.Where(rt => rt.UserId == userId && !rt.IsRevoked).ToListAsync();
        foreach (var token in tokens)
            token.IsRevoked = true;
    }
}
