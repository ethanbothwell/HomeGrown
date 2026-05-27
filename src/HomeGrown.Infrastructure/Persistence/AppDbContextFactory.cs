using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HomeGrown.Infrastructure.Persistence;

/// <summary>
/// Design-time factory used by EF Core tools (migrations) when the startup
/// project cannot be directly instantiated (e.g. due to missing env vars).
/// </summary>
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var opts = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(
                Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
                ?? "Host=localhost;Port=5432;Database=homegrown;Username=homegrown;Password=homegrown_dev")
            .Options;

        return new AppDbContext(opts);
    }
}
