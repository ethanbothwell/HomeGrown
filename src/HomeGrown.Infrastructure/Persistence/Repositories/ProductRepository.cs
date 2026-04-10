using HomeGrown.Core.Domain.Entities;
using HomeGrown.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeGrown.Infrastructure.Persistence.Repositories;

public class ProductRepository(AppDbContext context) : Repository<Product>(context), IProductRepository
{
    public async Task<IEnumerable<Product>> GetByFarmIdAsync(Guid farmId) =>
        await DbSet.Where(p => p.FarmId == farmId).Include(p => p.Farm).ToListAsync();

    public async Task<IEnumerable<Product>> SearchAsync(
        string? category, decimal? minPrice, decimal? maxPrice, bool? inStockOnly, string? sort)
    {
        var q = DbSet.Include(p => p.Farm).AsQueryable();

        if (!string.IsNullOrWhiteSpace(category))
            q = q.Where(p => p.Category.ToLower() == category.ToLower());

        if (minPrice.HasValue)
            q = q.Where(p => p.Price >= minPrice.Value);

        if (maxPrice.HasValue)
            q = q.Where(p => p.Price <= maxPrice.Value);

        if (inStockOnly == true)
            q = q.Where(p => p.InStock);

        q = sort switch
        {
            "price_asc"  => q.OrderBy(p => p.Price),
            "price_desc" => q.OrderByDescending(p => p.Price),
            "name"       => q.OrderBy(p => p.Name),
            _            => q.OrderBy(p => p.CreatedAt)
        };

        return await q.ToListAsync();
    }
}
