using HomeGrown.Core.Domain.Entities;
using HomeGrown.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeGrown.Infrastructure.Persistence.Repositories;

public class OrderRepository(AppDbContext context) : Repository<Order>(context), IOrderRepository
{
    public async Task<Order?> GetByIdWithItemsAsync(Guid id) =>
        await DbSet
            .Include(o => o.Items).ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.Id == id);

    public async Task<IEnumerable<Order>> GetByBuyerIdAsync(Guid buyerId) =>
        await DbSet
            .Include(o => o.Items).ThenInclude(oi => oi.Product)
            .Where(o => o.BuyerId == buyerId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
}
