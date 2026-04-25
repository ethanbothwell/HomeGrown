using HomeGrown.Core.Domain.Entities;
using HomeGrown.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeGrown.Infrastructure.Persistence.Repositories;

public class SubscriptionPlanRepository(AppDbContext context)
    : Repository<SubscriptionPlan>(context), ISubscriptionPlanRepository
{
    public async Task<IEnumerable<SubscriptionPlan>> GetActivePlansByFarmIdAsync(Guid farmId) =>
        await DbSet
            .Where(sp => sp.FarmId == farmId && sp.IsActive)
            .ToListAsync();

    public async Task<SubscriptionPlan?> GetByIdWithFarmAsync(Guid id) =>
        await DbSet
            .Include(sp => sp.Farm)
            .FirstOrDefaultAsync(sp => sp.Id == id);
}
