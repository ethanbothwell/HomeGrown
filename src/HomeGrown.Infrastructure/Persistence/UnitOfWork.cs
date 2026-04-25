using HomeGrown.Core.Domain.Entities;
using HomeGrown.Core.Domain.Interfaces;
using HomeGrown.Infrastructure.Persistence.Repositories;

namespace HomeGrown.Infrastructure.Persistence;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private IUserRepository? _users;
    private IFarmRepository? _farms;
    private IProductRepository? _products;
    private IOrderRepository? _orders;
    private IReviewRepository? _reviews;
    private IRefreshTokenRepository? _refreshTokens;
    private ISubscriberRepository? _subscribers;
    private ISubscriptionPlanRepository? _subscriptionPlans;
    private IFarmSubscriptionRepository? _farmSubscriptions;
    private IRepository<FarmPractice>? _farmPractices;

    public IUserRepository Users => _users ??= new UserRepository(context);
    public IFarmRepository Farms => _farms ??= new FarmRepository(context);
    public IProductRepository Products => _products ??= new ProductRepository(context);
    public IOrderRepository Orders => _orders ??= new OrderRepository(context);
    public IReviewRepository Reviews => _reviews ??= new ReviewRepository(context);
    public IRefreshTokenRepository RefreshTokens => _refreshTokens ??= new RefreshTokenRepository(context);
    public ISubscriberRepository Subscribers => _subscribers ??= new SubscriberRepository(context);
    public ISubscriptionPlanRepository SubscriptionPlans => _subscriptionPlans ??= new SubscriptionPlanRepository(context);
    public IFarmSubscriptionRepository FarmSubscriptions => _farmSubscriptions ??= new FarmSubscriptionRepository(context);
    public IRepository<FarmPractice> FarmPractices => _farmPractices ??= new Repository<FarmPractice>(context);

    public async Task<int> SaveChangesAsync() => await context.SaveChangesAsync();

    public void Dispose() => context.Dispose();
}
