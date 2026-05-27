using HomeGrown.Core.Domain.Entities;

namespace HomeGrown.Core.Domain.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetByFarmIdAsync(Guid farmId);
    Task<IEnumerable<Product>> SearchAsync(string? category, decimal? minPrice, decimal? maxPrice, bool? inStockOnly, string? sort);
}
