using HomeGrown.Models;

namespace HomeGrown.Services;

public interface IUserService
{
    Task<AppUser?> AuthenticateAsync(string email, string password);
    Task<AppUser?> GetByEmailAsync(string email);
    Task<AppUser?> GetByIdAsync(int id);
    Task<AppUser> RegisterAsync(string name, string email, string password, bool isFarmer);
}
