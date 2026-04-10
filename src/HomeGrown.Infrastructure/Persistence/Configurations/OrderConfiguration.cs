using HomeGrown.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeGrown.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Status).HasConversion<string>();
        builder.Property(o => o.Total).HasPrecision(10, 2);

        builder.HasMany(o => o.Items)
               .WithOne(oi => oi.Order)
               .HasForeignKey(oi => oi.OrderId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(o => o.Buyer)
               .WithMany(u => u.Orders)
               .HasForeignKey(o => o.BuyerId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
