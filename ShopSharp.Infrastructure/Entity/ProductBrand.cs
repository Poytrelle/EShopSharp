using ShopSharp.Infrastructure.EF;

namespace ShopSharp.Infrastructure.Entity;

public class ProductBrand : BaseEntity, IAggregateRoot
{
    public required string Name;
}
