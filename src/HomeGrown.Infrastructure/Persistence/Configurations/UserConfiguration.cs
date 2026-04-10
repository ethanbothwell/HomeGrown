using HomeGrown.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeGrown.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(256);
        builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Role).HasConversion<string>();
        builder.HasIndex(u => u.Email).IsUnique();
        builder.HasIndex(u => u.GoogleId).IsUnique().HasFilter("\"GoogleId\" IS NOT NULL");

        builder.HasOne(u => u.Farm)
               .WithOne(f => f.Owner)
               .HasForeignKey<Farm>(f => f.OwnerId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.RefreshTokens)
               .WithOne(rt => rt.User)
               .HasForeignKey(rt => rt.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
