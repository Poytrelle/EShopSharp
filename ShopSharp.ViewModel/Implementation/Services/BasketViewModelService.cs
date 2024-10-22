using ShopSharp.Infrastructure.EF;
using ShopSharp.Infrastructure.Entity;
using ShopSharp.Infrastructure.Services;
using ShopSharp.Infrastructure.Specifications;
using ShopSharp.ViewModel.Services;
using ShopSharp.ViewModel.Vm;

namespace ShopSharp.ViewModel.Implementation.Services;

internal class BasketViewModelService(
        IRepository<Basket> _basketRepository,
        IRepository<ProductItem> _itemRepository,
        IUriComposer _uriComposer,
        IBasketQueryService _basketQueryService) : IBasketViewModelService
{
    public async Task<BasketViewModel> GetOrCreateBasketForUser(string userName)
    {
        var basketSpec = new BasketWithItemsSpecification(userName);
        var basket = (await _basketRepository.FirstOrDefaultAsync(basketSpec));

        if (basket == null)
        {
            return await CreateBasketForUser(userName);
        }
        return await Map(basket);
    }

    private async Task<BasketViewModel> CreateBasketForUser(string userId)
    {
        var basket = new Basket() { BuyerId = userId };
        await _basketRepository.AddAsync(basket);

        return new BasketViewModel()
        {
            BuyerId = basket.BuyerId,
            Id = basket.Id,
        };
    }

    private async Task<List<BasketItemViewModel>> GetBasketItems(IReadOnlyCollection<BasketItem> basketItems)
    {
        var itemsSpecs = new ProductItemsSpecification(basketItems.Select(b => b.CatalogItemId).ToArray());
        var products = await _itemRepository.ListAsync(itemsSpecs);

        var items = basketItems.Select(basketItem =>
        {
            var item = products.First(c => c.Id == basketItem.CatalogItemId);

            var basketItemViewModel = new BasketItemViewModel
            {
                Id = basketItem.Id,
                UnitPrice = basketItem.UnitPrice,
                Quantity = basketItem.Quantity,
                CatalogItemId = basketItem.CatalogItemId,
                PictureUrl = _uriComposer.ComposePicUri(item.PictureUri),
                ProductName = item.Name
            };
            return basketItemViewModel;
        }).ToList();

        return items;
    }

    public async Task<BasketViewModel> Map(Basket basket)
    {
        return new BasketViewModel()
        {
            BuyerId = basket.BuyerId,
            Id = basket.Id,
            Items = await GetBasketItems(basket.Items)
        };
    }

    public async Task<int> CountTotalBasketItems(string username)
    {
        var counter = await _basketQueryService.CountTotalBasketItems(username);

        return counter;
    }
}
