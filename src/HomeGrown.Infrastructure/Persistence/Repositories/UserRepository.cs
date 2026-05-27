using HomeGrown.Core.Domain.Entities;
using HomeGrown.Core.Domain.Enums;
using HomeGrown.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeGrown.Infrastructure.Persistence.Repositories;

public class UserRepository(AppDbContext context) : Repository<User>(context), IUserRepository
{
    public async Task<User?> GetByEmailAsync(string email) =>
        await DbSet.FirstOrDefaultAsync(u => u.Email == email.ToLower());

    public async Task<User?> GetByGoogleIdAsync(string googleId) =>
        await DbSet.FirstOrDefaultAsync(u => u.GoogleId == googleId);

    public async Task<bool> EmailExistsAsync(string email) =>
        await DbSet.AnyAsync(u => u.Email == email.ToLower());

    public async Task<int> CountByRoleAsync(UserRole role, string? community = null)
    {
        var q = DbSet.Where(u => u.Role == role);
        if (community is not null)
            q = q.Where(u => u.Community == community);
        return await q.CountAsync();
    }

    public async Task<int> GetTotalCountAsync(string? community = null)
    {
        var q = DbSet.Where(u => u.Role == UserRole.Farmer || u.Role == UserRole.Buyer);
        if (community is not null)
            q = q.Where(u => u.Community == community);
        return await q.CountAsync();
    }

    public async Task<int> GetRegistrationPositionAsync(Guid userId, string? community = null)
    {
        var user = await DbSet.FindAsync(userId);
        if (user is null) return 0;
        var q = DbSet.Where(u =>
            (u.Role == UserRole.Farmer || u.Role == UserRole.Buyer) &&
            u.CreatedAt <= user.CreatedAt);
        if (community is not null)
            q = q.Where(u => u.Community == community);
        return await q.CountAsync();
    }

    public async Task<IEnumerable<string>> GetAllEmailsAsync(string? community = null)
    {
        var q = DbSet.Where(u => u.Role == UserRole.Farmer || u.Role == UserRole.Buyer);
        if (community is not null)
            q = q.Where(u => u.Community == community);
        return await q.Select(u => u.Email).ToListAsync();
    }

    public async Task<IEnumerable<(string Community, int FarmerCount, int BuyerCount)>> GetCommunitySummariesAsync()
    {
        var rows = await DbSet
            .Where(u =>
                (u.Role == UserRole.Farmer || u.Role == UserRole.Buyer) &&
                u.Community != null)
            .GroupBy(u => u.Community!)
            .Select(g => new
            {
                Community = g.Key,
                FarmerCount = g.Count(u => u.Role == UserRole.Farmer),
                BuyerCount  = g.Count(u => u.Role == UserRole.Buyer),
            })
            .ToListAsync();

        return rows.Select(r => (r.Community, r.FarmerCount, r.BuyerCount));
    }
}
