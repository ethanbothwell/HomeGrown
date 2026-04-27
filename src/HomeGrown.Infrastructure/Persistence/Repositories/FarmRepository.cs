using HomeGrown.Core.Domain.Entities;
using HomeGrown.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeGrown.Infrastructure.Persistence.Repositories;

public class FarmRepository(AppDbContext context) : Repository<Farm>(context), IFarmRepository
{
    public async Task<Farm?> GetByIdWithDetailsAsync(Guid id) =>
        await DbSet
            .Include(f => f.Owner)
            .Include(f => f.Products)
            .Include(f => f.Reviews).ThenInclude(r => r.User)
            .Include(f => f.Practices)
            .Include(f => f.SubscriptionPlans.Where(p => p.IsActive))
            .FirstOrDefaultAsync(f => f.Id == id);

    public async Task<Farm?> GetByOwnerIdAsync(Guid ownerId) =>
        await DbSet.FirstOrDefaultAsync(f => f.OwnerId == ownerId);

    public async Task<IEnumerable<Farm>> GetAllWithDetailsAsync() =>
        await DbSet
            .Include(f => f.Owner)
            .Include(f => f.Practices)
            .Where(f => f.IsActive)
            .ToListAsync();

    public async Task<IEnumerable<Farm>> SearchAsync(string? category, string? state, string? query)
    {
        var q = DbSet.Include(f => f.Owner).Include(f => f.Practices).Where(f => f.IsActive);

        if (!string.IsNullOrWhiteSpace(state))
            q = q.Where(f => f.State != null && f.State.ToLower() == state.ToLower());

        if (!string.IsNullOrWhiteSpace(query))
            q = q.Where(f => f.Name.ToLower().Contains(query.ToLower()) ||
                              (f.Bio != null && f.Bio.ToLower().Contains(query.ToLower())));

        return await q.ToListAsync();
    }
}
