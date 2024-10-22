using Microsoft.EntityFrameworkCore;
using ShopSharp.Domain.Data;
using ShopSharp.Infrastructure.Services;

namespace ShopSharp.Domain.Implementation.Services;

public class BasketQueryService(CatalogContext _dbContext) : IBasketQueryService
{
    /// <summary>
    /// This method performs the sum on the database rather than in memory
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    public async Task<int> CountTotalBasketItems(string username)
    {
        return await _dbContext.Baskets
            .Where(basket => basket.BuyerId == username)
            .SelectMany(item => item.Items)
            .SumAsync(sum => sum.Quantity);
    }
}
