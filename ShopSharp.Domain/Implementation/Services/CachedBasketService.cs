using System.Text.Json;
using Ardalis.Result;
using Microsoft.AspNetCore.Http;
using ShopSharp.Domain.Constants;
using ShopSharp.Infrastructure.Entity;
using ShopSharp.Infrastructure.Services;

namespace ShopSharp.Domain.Implementation.Services;

public class CachedBasketService(
    IHttpContextAccessor _httpContextAccessor,
    IAppLogger<CachedBasketService> _logger) : IBasketService
{
    public Task<Basket> AddItemToBasket(string _, int catalogItemId, decimal price, int quantity = 1)
    {
        // Get the basket from the cookie
        var basket = GetOrCreateFromCookie();

        // Add the item to the basket
        basket.AddItem(catalogItemId, price, quantity);

        // Save the basket to the cookie
        SaveBasketToCookie(basket);

        return Task.FromResult(basket);
    }

    public Task DeleteBasketAsync(int basketId)
    {
        // remove the basket from the cookie
        _httpContextAccessor.HttpContext?.Response.Cookies.Delete(BasketCache.BasketCookieKey);
        return Task.CompletedTask;
    }

    public Task<Result<Basket>> SetQuantities(int basketId, Dictionary<string, int> quantities)
    {
        // Get the basket from the cookie
        var basket = GetOrCreateFromCookie();
        // Update the quantities
        foreach (var item in basket.Items)
        {
            if (quantities.TryGetValue(item.Id.ToString(), out var quantity))
            {
                item.SetQuantity(quantity);
            }
        }
        // Remove empty items
        basket.RemoveEmptyItems();
        // Save the basket to the cookie
        SaveBasketToCookie(basket);

        return Task.FromResult(Result<Basket>.Success(basket));
    }

    //

    private Basket GetOrCreateFromCookie()
    {
        List<BasketItem>? items = null;
        var cookies = _httpContextAccessor.HttpContext?.Request.Cookies;
        if (cookies != null && cookies.TryGetValue(BasketCache.BasketCookieKey, out var basketJson))
        {
            items = JsonSerializer.Deserialize<List<BasketItem>>(basketJson);
        }

        var basket = new Basket() { BuyerId = BasketCache.BasketDefaultUser };
        items?.ForEach(item => basket.AddItem(item.CatalogItemId, item.UnitPrice, item.Quantity));

        return basket;
    }

    private void SaveBasketToCookie(Basket basket)
    {
        var basketJson = JsonSerializer.Serialize(basket.Items);
        _httpContextAccessor.HttpContext?.Response.Cookies.Append(BasketCache.BasketCookieKey, basketJson);
    }
}
