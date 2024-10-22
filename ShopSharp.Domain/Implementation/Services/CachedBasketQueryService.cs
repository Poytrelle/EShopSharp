using System.Text.Json;
using Microsoft.AspNetCore.Http;
using ShopSharp.Domain.Constants;
using ShopSharp.Infrastructure.Entity;
using ShopSharp.Infrastructure.Services;

namespace ShopSharp.Domain.Implementation.Services;

public class CachedBasketQueryService(
    IHttpContextAccessor _httpContextAccessor) : IBasketQueryService
{
    public Task<int> CountTotalBasketItems(string _)
    {
        var basket = GetFromCookie();
        return Task.FromResult(basket?.Items?.Sum(item => item.Quantity) ?? 0);
    }

    private Basket? GetFromCookie()
    {
        Basket? basket = null;
        var cookies = _httpContextAccessor.HttpContext?.Request.Cookies;
        if (cookies != null && cookies.TryGetValue(BasketCache.BasketCookieKey, out var basketJson))
        {
            basket = JsonSerializer.Deserialize<Basket>(basketJson);
        }
        return basket;
    }
}
