using ShopSharp.Infrastructure.EF;

namespace ShopSharp.Infrastructure.Entity;

public class OrderItem : BaseEntity
{
    public required ProductItemOrdered ItemOrdered { get; init; }
    public required decimal UnitPrice { get; init; }
    public required int Units { get; init; }
}
