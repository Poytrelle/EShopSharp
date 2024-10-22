using Ardalis.Result;
using ShopSharp.Infrastructure.Entity;

namespace ShopSharp.Infrastructure.Services;

public interface IBasketService
{
    Task TransferBasketAsync(string anonymousId, string userName);
    Task<Basket> AddItemToBasket(string username, int catalogItemId, decimal price, int quantity = 1);
    Task<Result<Basket>> SetQuantities(int basketId, Dictionary<string, int> quantities);
    Task DeleteBasketAsync(int basketId);
}
