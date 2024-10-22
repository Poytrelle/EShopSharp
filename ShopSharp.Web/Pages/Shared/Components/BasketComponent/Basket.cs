using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopSharp.Domain.Identity;
using ShopSharp.ViewModel.Services;
using ShopSharp.ViewModel.Vm;
using ShopSharp.Web.Constants;

namespace ShopSharp.Web.Pages.Shared.Components.BasketComponent;

public class Basket : ViewComponent
{
    private readonly IBasketViewModelService _basketService;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public Basket(IBasketViewModelService basketService,
                    SignInManager<ApplicationUser> signInManager)
    {
        _basketService = basketService;
        _signInManager = signInManager;
    }

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
        if (_signInManager.IsSignedIn(HttpContext.User))
        {
            Debug.Assert(!string.IsNullOrEmpty(User?.Identity?.Name));
            return await _basketService.CountTotalBasketItems(User.Identity.Name);
        }

        string? anonymousId = GetAnonymousIdFromCookie();
        if (anonymousId == null)
            return 0;

        return await _basketService.CountTotalBasketItems(anonymousId);
    }

    private string? GetAnonymousIdFromCookie()
    {
        if (Request.Cookies.TryGetValue(BasketConstants.BasketCookieName, out var id) &&
            Guid.TryParse(id, out var _))
        {
            return id;
        }
        return null;
    }
}
