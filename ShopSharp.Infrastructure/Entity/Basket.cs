using ShopSharp.Infrastructure.EF;

namespace ShopSharp.Infrastructure.Entity;

public class Basket : BaseEntity, IAggregateRoot
{
    public required string BuyerId { get; init; }
    private readonly List<BasketItem> _items = [];

    public IReadOnlyCollection<BasketItem> Items => _items.AsReadOnly();
    public int TotalItems => _items.Sum(i => i.Quantity);

    public void AddItem(int catalogItemId, decimal unitPrice, int quantity = 1)
    {
        if (!Items.Any(i => i.CatalogItemId == catalogItemId))
        {
            var item = new BasketItem() { CatalogItemId = catalogItemId, UnitPrice = unitPrice };
            item.SetQuantity(quantity);
            _items.Add(item);
            return;
        }
        var existingItem = Items.First(i => i.CatalogItemId == catalogItemId);
        existingItem.AddQuantity(quantity);
    }

    public void RemoveEmptyItems()
    {
        _items.RemoveAll(i => i.Quantity == 0);
    }
}
