using HomeGrown.Core.Domain.Entities;

namespace HomeGrown.Core.Domain.Interfaces;

public interface IReviewRepository : IRepository<Review>
{
    Task<IEnumerable<Review>> GetByFarmIdAsync(Guid farmId);
    Task<bool> UserHasReviewedFarmAsync(Guid userId, Guid farmId);
}
