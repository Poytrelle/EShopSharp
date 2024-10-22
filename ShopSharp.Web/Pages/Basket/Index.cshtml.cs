using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopSharp.Infrastructure.EF;
using ShopSharp.Infrastructure.Entity;
using ShopSharp.Infrastructure.Services;
using ShopSharp.ViewModel.Services;
using ShopSharp.ViewModel.Vm;
using ShopSharp.Web.Constants;

namespace ShopSharp.Web.Pages.Basket;

public class IndexModel : PageModel
{
    private readonly IBasketService _basketService;
    private readonly IBasketViewModelService _basketViewModelService;
    private readonly IRepository<ProductItem> _itemRepository;

    public IndexModel(IBasketService basketService,
        IBasketViewModelService basketViewModelService,
        IRepository<ProductItem> itemRepository)
    {
        _basketService = basketService;
        _basketViewModelService = basketViewModelService;
        _itemRepository = itemRepository;
    }

    public BasketViewModel DataContext { get; set; } = new();

    public async Task OnGet()
    {
        DataContext = await _basketViewModelService.GetOrCreateBasketForUser(GetOrSetBasketCookieAndUserName());
    }

    public async Task<IActionResult> OnPost(ProductItemViewModel productDetails)
    {
        if (productDetails?.Id == null)
        {
            return RedirectToPage("/Index");
        }

        var item = await _itemRepository.GetByIdAsync(productDetails.Id);
        if (item == null)
        {
            return RedirectToPage("/Index");
        }

        var username = GetOrSetBasketCookieAndUserName();
        var basket = await _basketService.AddItemToBasket(username,
            productDetails.Id, item.Price);

        DataContext = await _basketViewModelService.Map(basket);

        return RedirectToPage();
    }

    public async Task OnPostUpdate(IEnumerable<BasketItemViewModel> items)
    {
        if (!ModelState.IsValid)
        {
            return;
        }

        var basketView = await _basketViewModelService.GetOrCreateBasketForUser(GetOrSetBasketCookieAndUserName());
        var updateModel = items.ToDictionary(b => b.Id.ToString(), b => b.Quantity);
        var basket = await _basketService.SetQuantities(basketView.Id, updateModel);
        DataContext = await _basketViewModelService.Map(basket);
    }

    private string GetOrSetBasketCookieAndUserName()
    {
        Debug.Assert(Request.HttpContext.User.Identity != null);
        string? userName = null;

        if (Request.HttpContext.User.Identity.IsAuthenticated)
        {
            Debug.Assert(Request.HttpContext.User.Identity.Name != null);
                return Request.HttpContext.User.Identity.Name!;
        }

        if (Request.Cookies.TryGetValue(BasketConstants.BasketCookieName, out userName))
        {
            if (!Request.HttpContext.User.Identity.IsAuthenticated)
            {
                if (!Guid.TryParse(userName, out var _))
                {
                    userName = null;
                }
            }
        }
        if (userName != null)
            return userName;

        userName = Guid.NewGuid().ToString();
        var cookieOptions = new CookieOptions
        {
            IsEssential = true,
            Expires = DateTime.Today.AddYears(10)
        };
        Response.Cookies.Append(BasketConstants.BasketCookieName, userName, cookieOptions);

        return userName;
    }
}
