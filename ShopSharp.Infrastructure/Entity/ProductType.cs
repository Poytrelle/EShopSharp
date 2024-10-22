using ShopSharp.Infrastructure.EF;

namespace ShopSharp.Infrastructure.Entity;

public class ProductType : BaseEntity, IAggregateRoot
{
    public required string Name;
}
