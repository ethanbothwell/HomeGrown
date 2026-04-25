using HomeGrown.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeGrown.Infrastructure.Persistence.Configurations;

public class FarmSubscriptionConfiguration : IEntityTypeConfiguration<FarmSubscription>
{
    public void Configure(EntityTypeBuilder<FarmSubscription> builder)
    {
        builder.HasKey(fs => fs.Id);

        builder.Property(fs => fs.Status)
            .HasConversion<string>();

        builder.HasOne(fs => fs.Buyer)
            .WithMany(u => u.FarmSubscriptions)
            .HasForeignKey(fs => fs.BuyerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(fs => fs.BuyerId);
        builder.HasIndex(fs => fs.SubscriptionPlanId);
    }
}
