using HomeGrown.Core.Domain.Entities;
using HomeGrown.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeGrown.Infrastructure.Persistence.Repositories;

public class UserRepository(AppDbContext context) : Repository<User>(context), IUserRepository
{
    public async Task<User?> GetByEmailAsync(string email) =>
        await DbSet.FirstOrDefaultAsync(u => u.Email == email.ToLower());

    public async Task<User?> GetByGoogleIdAsync(string googleId) =>
        await DbSet.FirstOrDefaultAsync(u => u.GoogleId == googleId);

    public async Task<bool> EmailExistsAsync(string email) =>
        await DbSet.AnyAsync(u => u.Email == email.ToLower());
}
