using HomeGrown.Core.Domain.Entities;

namespace HomeGrown.Core.Domain.Interfaces;

public interface IFarmSubscriptionRepository : IRepository<FarmSubscription>
{
    Task<IEnumerable<FarmSubscription>> GetByBuyerIdAsync(Guid buyerId);
    Task<FarmSubscription?> GetByIdWithPlanAsync(Guid id);
    Task<int> GetActiveSubscriberCountAsync(Guid planId);
}
