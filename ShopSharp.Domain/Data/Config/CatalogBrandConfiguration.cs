using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopSharp.Infrastructure.Entity;

namespace ShopSharp.Domain.Data.Config;

public class CatalogBrandConfiguration : IEntityTypeConfiguration<ProductBrand>
{
    public void Configure(EntityTypeBuilder<ProductBrand> builder)
    {
        builder.HasKey(ci => ci.Id);

        builder.Property(ci => ci.Id)
           .UseHiLo("catalog_brand_hilo")
           .IsRequired();

        builder.Property(cb => cb.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
