using ShopSharp.Infrastructure.Entity;

namespace ShopSharp.Infrastructure.Services;

public interface IOrderService
{
    Task CreateOrderAsync(int basketId, Address shippingAddress);
}
