using HomeGrown.Data;
using HomeGrown.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeGrown.Services;

public class DbUserService : IUserService
{
    private readonly HomeGrownDbContext _db;

    public DbUserService(HomeGrownDbContext db)
    {
        _db = db;
    }

    public async Task<AppUser?> AuthenticateAsync(string email, string password)
    {
        var user = await _db.Users
            .FirstOrDefaultAsync(u => u.Email.ToLower() == email.Trim().ToLower());

        if (user == null)
            return null;

        return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash) ? user : null;
    }

    public async Task<AppUser?> GetByEmailAsync(string email)
    {
        return await _db.Users
            .FirstOrDefaultAsync(u => u.Email.ToLower() == email.Trim().ToLower());
    }

    public async Task<AppUser?> GetByIdAsync(int id)
    {
        return await _db.Users.FindAsync(id);
    }

    public async Task<AppUser> RegisterAsync(string name, string email, string password, bool isFarmer)
    {
        var user = new AppUser
        {
            Name = name.Trim(),
            Email = email.Trim().ToLower(),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            IsFarmer = isFarmer,
            CreatedAt = DateTime.UtcNow
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }
}
