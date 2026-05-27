using HomeGrown.Core.Domain.Entities;
using HomeGrown.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeGrown.Infrastructure.Persistence.Repositories;

public class ReviewRepository(AppDbContext context) : Repository<Review>(context), IReviewRepository
{
    public async Task<IEnumerable<Review>> GetByFarmIdAsync(Guid farmId) =>
        await DbSet
            .Include(r => r.User)
            .Where(r => r.FarmId == farmId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();

    public async Task<bool> UserHasReviewedFarmAsync(Guid userId, Guid farmId) =>
        await DbSet.AnyAsync(r => r.UserId == userId && r.FarmId == farmId);
}
