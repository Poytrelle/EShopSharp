using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopSharp.Infrastructure.Entity;

namespace ShopSharp.Domain.Data.Config;

public class ProductItemConfiguration : IEntityTypeConfiguration<ProductItem>
{
    public void Configure(EntityTypeBuilder<ProductItem> builder)
    {
        builder.ToTable("Catalog");

        builder.Property(ci => ci.Id)
            .UseHiLo("catalog_hilo")
            .IsRequired();

        builder.Property(ci => ci.Name)
            .IsRequired(true)
            .HasMaxLength(50);

        builder.Property(ci => ci.Price)
            .IsRequired(true)
            .HasColumnType("decimal(18,2)");

        builder.Property(ci => ci.PictureUri)
            .IsRequired(false);

        builder.HasOne(ci => ci.Brand)
            .WithMany()
            .HasForeignKey(ci => ci.BrandId);

        builder.HasOne(ci => ci.Type)
            .WithMany()
            .HasForeignKey(ci => ci.TypeId);
    }
}
