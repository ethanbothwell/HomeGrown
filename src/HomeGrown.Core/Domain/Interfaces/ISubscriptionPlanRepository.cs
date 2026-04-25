using HomeGrown.Core.Domain.Entities;

namespace HomeGrown.Core.Domain.Interfaces;

public interface ISubscriptionPlanRepository : IRepository<SubscriptionPlan>
{
    Task<IEnumerable<SubscriptionPlan>> GetActivePlansByFarmIdAsync(Guid farmId);
    Task<SubscriptionPlan?> GetByIdWithFarmAsync(Guid id);
}
