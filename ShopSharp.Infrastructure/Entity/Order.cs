using ShopSharp.Infrastructure.EF;

namespace ShopSharp.Infrastructure.Entity;

public class Order : BaseEntity, IAggregateRoot
{
    public required string BuyerId { get; init; }
    public DateTimeOffset OrderDate { get; init; } = DateTimeOffset.Now;
    public required Address ShipToAddress { get; init; }

    public List<OrderItem> OrderItems { get; init; } = [];

    public decimal Total => OrderItems.Sum(item => item.UnitPrice * item.Units);
}
