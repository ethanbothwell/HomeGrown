namespace HomeGrown.Core.Domain.Interfaces;

/// <summary>
/// Unit of Work — groups all repositories under a single database transaction.
/// Call SaveChangesAsync() once after all mutations to commit atomically.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IFarmRepository Farms { get; }
    IProductRepository Products { get; }
    IOrderRepository Orders { get; }
    IReviewRepository Reviews { get; }
    IRefreshTokenRepository RefreshTokens { get; }
    ISubscriberRepository Subscribers { get; }

    Task<int> SaveChangesAsync();
}
