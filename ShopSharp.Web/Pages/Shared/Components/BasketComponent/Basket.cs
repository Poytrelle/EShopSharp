using Microsoft.AspNetCore.Mvc;
using ShopSharp.ViewModel.Services;
using ShopSharp.ViewModel.Vm;
using ShopSharp.Web.Constants;

namespace ShopSharp.Web.Pages.Shared.Components.BasketComponent;

public class Basket(IBasketViewModelService _basketService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var vm = new BasketComponentViewModel
        {
            ItemsCount = await CountTotalBasketItems()
        };
        return View(vm);
    }

    private async Task<int> CountTotalBasketItems()
    {
        var userName = GetUserName();
        return userName != null ? await _basketService.CountTotalBasketItems(userName) : 0;
    }

    private string? GetUserName()
    {
        if (Request.Cookies.TryGetValue(BasketConstants.BasketCookieName, out var id) &&
            Guid.TryParse(id, out var _))
        {
            return id;
        }
        return null;
    }
}
