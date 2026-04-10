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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.PasswordHash).HasMaxLength(200);
            });
        }
    }
}
