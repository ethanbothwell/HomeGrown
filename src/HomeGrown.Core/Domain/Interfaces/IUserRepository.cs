using HomeGrown.Core.Domain.Entities;
using HomeGrown.Core.Domain.Enums;

namespace HomeGrown.Core.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByGoogleIdAsync(string googleId);
    Task<bool> EmailExistsAsync(string email);
    Task<int> CountByRoleAsync(UserRole role, string? community = null);
    Task<int> GetTotalCountAsync(string? community = null);
    Task<int> GetRegistrationPositionAsync(Guid userId, string? community = null);
    Task<IEnumerable<string>> GetAllEmailsAsync(string? community = null);
    Task<IEnumerable<(string Community, int FarmerCount, int BuyerCount)>> GetCommunitySummariesAsync();
}
