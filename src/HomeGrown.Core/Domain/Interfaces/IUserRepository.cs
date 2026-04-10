using HomeGrown.Core.Domain.Entities;

namespace HomeGrown.Core.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByGoogleIdAsync(string googleId);
    Task<bool> EmailExistsAsync(string email);
}
