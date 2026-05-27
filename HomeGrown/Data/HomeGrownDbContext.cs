using HomeGrown.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeGrown.Data
{
    public class HomeGrownDbContext : DbContext
    {
        public HomeGrownDbContext(DbContextOptions<HomeGrownDbContext> options)
            : base(options)
        {
        }

        public DbSet<Subscriber> Subscribers => Set<Subscriber>();
        public DbSet<AppUser> Users => Set<AppUser>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<UserFollowedFarm> UserFollowedFarms => Set<UserFollowedFarm>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.PasswordHash).HasMaxLength(200);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasMany(o => o.Items)
                    .WithOne()
                    .HasForeignKey(i => i.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<UserFollowedFarm>(entity =>
            {
                entity.HasIndex(f => new { f.UserId, f.FarmId }).IsUnique();
            });
        }
    }
}
