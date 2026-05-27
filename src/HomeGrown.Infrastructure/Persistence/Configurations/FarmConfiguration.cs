using HomeGrown.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeGrown.Infrastructure.Persistence.Configurations;

public class FarmConfiguration : IEntityTypeConfiguration<Farm>
{
    public void Configure(EntityTypeBuilder<Farm> builder)
    {
        builder.HasKey(f => f.Id);
        builder.Property(f => f.Name).IsRequired().HasMaxLength(200);

        builder.HasMany(f => f.Products)
               .WithOne(p => p.Farm)
               .HasForeignKey(p => p.FarmId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(f => f.Reviews)
               .WithOne(r => r.Farm)
               .HasForeignKey(r => r.FarmId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(f => f.Practices)
               .WithOne(fp => fp.Farm)
               .HasForeignKey(fp => fp.FarmId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
