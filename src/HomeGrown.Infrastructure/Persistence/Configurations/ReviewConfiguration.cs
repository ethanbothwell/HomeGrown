using HomeGrown.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeGrown.Infrastructure.Persistence.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Text).IsRequired().HasMaxLength(2000);

        // One review per user per farm
        builder.HasIndex(r => new { r.UserId, r.FarmId }).IsUnique();

        builder.HasOne(r => r.User)
               .WithMany(u => u.Reviews)
               .HasForeignKey(r => r.UserId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
