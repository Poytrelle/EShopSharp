using Microsoft.EntityFrameworkCore;
using ShopSharp.Domain.Data;
using ShopSharp.Infrastructure.Services;

namespace ShopSharp.Domain.Implementation.Services;

public class DBBasketQueryService(CatalogContext _dbContext) : IBasketQueryService
{
    public async Task<int> CountTotalBasketItems(string username)
    {
        return await _dbContext.Baskets
            .Where(basket => basket.BuyerId == username)
            .SelectMany(item => item.Items)
            .SumAsync(sum => sum.Quantity);
    }
}
