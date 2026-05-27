using HomeGrown.Core.Domain.Entities;

namespace HomeGrown.Core.Domain.Interfaces;

public interface ISubscriberRepository : IRepository<Subscriber>
{
    Task<bool> EmailExistsAsync(string email);
}
