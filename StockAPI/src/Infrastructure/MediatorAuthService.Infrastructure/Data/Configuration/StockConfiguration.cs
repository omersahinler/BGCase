using StockAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StockAPI.Infrastructure.Data.Configuration;

public class StockConfiguration : IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> builder)
    {
        builder.Property(x => x.ProductCode)
            .HasMaxLength(5)
            .IsRequired();

        builder.Property(x => x.VariantCode)
            .HasMaxLength(5)
            .IsRequired();

        builder.Property(x => x.Quantity)
            .IsRequired();
    }
}