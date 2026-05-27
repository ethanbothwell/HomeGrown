using HomeGrown.Core.Domain.Entities;
using HomeGrown.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeGrown.Infrastructure.Persistence.Repositories;

public class SubscriberRepository(AppDbContext context) : Repository<Subscriber>(context), ISubscriberRepository
{
    public async Task<bool> EmailExistsAsync(string email) =>
        await DbSet.AnyAsync(s => s.Email == email.ToLower());
}
