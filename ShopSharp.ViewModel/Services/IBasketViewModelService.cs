using ShopSharp.Infrastructure.Entity;
using ShopSharp.ViewModel.Vm;

namespace ShopSharp.ViewModel.Services;

public interface IBasketViewModelService
{
    Task<BasketViewModel> GetOrCreateBasketForUser(string userName);
    Task<int> CountTotalBasketItems(string username);
    Task<BasketViewModel> Map(Basket basket);
}
