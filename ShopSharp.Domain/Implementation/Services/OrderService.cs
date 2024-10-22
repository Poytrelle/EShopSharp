using System.Diagnostics;
using ShopSharp.Infrastructure.EF;
using ShopSharp.Infrastructure.Entity;
using ShopSharp.Infrastructure.Services;
using ShopSharp.Infrastructure.Specifications;

namespace ShopSharp.Domain.Implementation.Services;

public class OrderService : IOrderService
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IUriComposer _uriComposer;
    private readonly IRepository<Basket> _basketRepository;
    private readonly IRepository<ProductItem> _itemRepository;

    public OrderService(IRepository<Basket> basketRepository,
        IRepository<ProductItem> itemRepository,
        IRepository<Order> orderRepository,
        IUriComposer uriComposer)
    {
        _orderRepository = orderRepository;
        _uriComposer = uriComposer;
        _basketRepository = basketRepository;
        _itemRepository = itemRepository;
    }

    public async Task CreateOrderAsync(int basketId, Address shippingAddress)
    {
        var basketSpec = new BasketWithItemsSpecification(basketId);
        var basket = await _basketRepository.FirstOrDefaultAsync(basketSpec);

        Debug.Assert(basket != null, "Basket not found.");
        Debug.Assert(basket.Items.Count != 0, "Basket is empty.");

        var catalogItemsSpecification = new ProductItemsSpecification(basket.Items.Select(item => item.CatalogItemId).ToArray());
        var catalogItems = await _itemRepository.ListAsync(catalogItemsSpecification);

        var items = basket.Items.Select(basketItem =>
        {
            var catalogItem = catalogItems.First(c => c.Id == basketItem.CatalogItemId);
            var itemOrdered = new ProductItemOrdered()
            {
                CatalogItemId = catalogItem.Id,
                ProductName = catalogItem.Name,
                PictureUri = _uriComposer.ComposePicUri(catalogItem.PictureUri)
            };
            var orderItem = new OrderItem()
            {
                ItemOrdered = itemOrdered,
                UnitPrice = basketItem.UnitPrice,
                Units = basketItem.Quantity
            };
            return orderItem;
        }).ToList();

        var order = new Order() { BuyerId = basket.BuyerId, ShipToAddress = shippingAddress, OrderItems = items };
        await _orderRepository.AddAsync(order);
    }
}
