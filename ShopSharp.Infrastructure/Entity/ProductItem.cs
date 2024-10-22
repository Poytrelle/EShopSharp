using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using ShopSharp.Infrastructure.EF;

namespace ShopSharp.Infrastructure.Entity;

public class ProductItem : BaseEntity, IAggregateRoot
{
    public required string Name;
    public required string Description;
    public required decimal Price;
    public required string PictureUri;
    public required int TypeId;
    public required int BrandId;

    public ProductType? Type { get; }
    public ProductBrand? Brand { get; }

    public void UpdateDetails(Details details)
    {
        Name = details.Name;
        Description = details.Description;
        Price = details.Price;
    }

    public struct Details
    {
        public required string Name;
        public required string Description;
        [Range(0, double.MaxValue)]
        public decimal Price;
    }
}
