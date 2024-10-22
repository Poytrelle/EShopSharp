using System.Diagnostics;
using ShopSharp.Infrastructure.EF;

namespace ShopSharp.Infrastructure.Entity;

public class BasketItem : BaseEntity, IAggregateRoot
{
    public required decimal UnitPrice { get; init; }
    public int Quantity { get; private set; } = 0;
    public required int CatalogItemId { get; init; }
    public int BasketId { get; init; }

    public void AddQuantity(int quantity)
    {
        Debug.Assert(quantity > 0 && quantity < int.MaxValue);
        Quantity += quantity;
    }

    public void SetQuantity(int quantity)
    {
        Debug.Assert(quantity > 0 && quantity < int.MaxValue);
        Quantity = quantity;
    }
}
