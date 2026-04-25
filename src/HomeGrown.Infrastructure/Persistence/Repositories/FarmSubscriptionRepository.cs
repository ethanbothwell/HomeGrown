using HomeGrown.Core.Domain.Entities;
using HomeGrown.Core.Domain.Enums;
using HomeGrown.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeGrown.Infrastructure.Persistence.Repositories;

public class FarmSubscriptionRepository(AppDbContext context)
    : Repository<FarmSubscription>(context), IFarmSubscriptionRepository
{
    public async Task<IEnumerable<FarmSubscription>> GetByBuyerIdAsync(Guid buyerId) =>
        await DbSet
            .Include(fs => fs.Plan)
            .ThenInclude(sp => sp.Farm)
            .Where(fs => fs.BuyerId == buyerId)
            .OrderByDescending(fs => fs.CreatedAt)
            .ToListAsync();

    public async Task<FarmSubscription?> GetByIdWithPlanAsync(Guid id) =>
        await DbSet
            .Include(fs => fs.Plan)
            .ThenInclude(sp => sp.Farm)
            .FirstOrDefaultAsync(fs => fs.Id == id);

    public async Task<int> GetActiveSubscriberCountAsync(Guid planId) =>
        await DbSet
            .CountAsync(fs => fs.SubscriptionPlanId == planId
                           && fs.Status == SubscriptionStatus.Active);
}
