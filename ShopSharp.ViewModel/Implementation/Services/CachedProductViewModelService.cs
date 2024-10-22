using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using ShopSharp.ViewModel.Constants;
using ShopSharp.ViewModel.Implementation.Helpers;
using ShopSharp.ViewModel.Services;
using ShopSharp.ViewModel.Vm;

namespace ShopSharp.ViewModel.Implementation.Services;

internal class CachedProductViewModelService(
    IMemoryCache _cache,
    ProductViewModelService _catalogViewModelService) : IProductViewModelService
{
    public async Task<IEnumerable<SelectListItem>> GetBrands()
    {
        return await _cache.GetOrCreateAsync(CacheHelpers.GenerateBrandsCacheKey(), async entry =>
                {
                    entry.SlidingExpiration = CacheHelpers.DefaultCacheDuration;
                    return await _catalogViewModelService.GetBrands();
                }) ?? new List<SelectListItem>();
    }

    public async Task<ProductViewModel> GetCatalogItems(int pageIndex, int itemsPage, int? brandId, int? typeId)
    {
        var cacheKey = CacheHelpers.GenerateItemCacheKey(pageIndex, ProductConstants.ItemsPerPage, brandId, typeId);

        return await _cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.SlidingExpiration = CacheHelpers.DefaultCacheDuration;
            return await _catalogViewModelService.GetCatalogItems(pageIndex, itemsPage, brandId, typeId);
        }) ?? new();
    }

    public async Task<IEnumerable<SelectListItem>> GetTypes()
    {
        return await _cache.GetOrCreateAsync(CacheHelpers.GenerateTypesCacheKey(), async entry =>
        {
            entry.SlidingExpiration = CacheHelpers.DefaultCacheDuration;
            return await _catalogViewModelService.GetTypes();
        }) ?? new List<SelectListItem>();
    }
}
