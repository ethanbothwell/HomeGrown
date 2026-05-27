using HomeGrown.Core.Domain.Entities;
using HomeGrown.Core.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeGrown.Infrastructure.Persistence.Configurations;

public class SubscriptionPlanConfiguration : IEntityTypeConfiguration<SubscriptionPlan>
{
    public void Configure(EntityTypeBuilder<SubscriptionPlan> builder)
    {
        builder.HasKey(sp => sp.Id);

        builder.Property(sp => sp.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(sp => sp.Price)
            .HasPrecision(10, 2);

        builder.Property(sp => sp.Frequency)
            .HasConversion<string>();

        builder.HasOne(sp => sp.Farm)
            .WithMany(f => f.SubscriptionPlans)
            .HasForeignKey(sp => sp.FarmId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(sp => sp.Subscriptions)
            .WithOne(fs => fs.Plan)
            .HasForeignKey(fs => fs.SubscriptionPlanId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(sp => sp.FarmId);
    }
}
