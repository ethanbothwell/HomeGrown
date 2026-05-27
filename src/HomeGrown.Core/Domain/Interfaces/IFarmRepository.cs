using HomeGrown.Core.Domain.Entities;

namespace HomeGrown.Core.Domain.Interfaces;

public interface IFarmRepository : IRepository<Farm>
{
    Task<Farm?> GetByIdWithDetailsAsync(Guid id);       // includes products, reviews, practices
    Task<Farm?> GetByOwnerIdAsync(Guid ownerId);
    Task<IEnumerable<Farm>> GetAllWithDetailsAsync();
    Task<IEnumerable<Farm>> SearchAsync(string? category, string? state, string? query);
}
