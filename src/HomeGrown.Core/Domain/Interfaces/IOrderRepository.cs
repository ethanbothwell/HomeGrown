using HomeGrown.Core.Domain.Entities;

namespace HomeGrown.Core.Domain.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    Task<Order?> GetByIdWithItemsAsync(Guid id);
    Task<IEnumerable<Order>> GetByBuyerIdAsync(Guid buyerId);
}
