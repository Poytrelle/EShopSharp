using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopSharp.Domain.Identity;
using ShopSharp.Infrastructure.Entity;
using ShopSharp.Infrastructure.Exceptions;
using ShopSharp.Infrastructure.Services;
using ShopSharp.ViewModel.Services;
using ShopSharp.ViewModel.Vm;
using ShopSharp.Web.Constants;

namespace ShopSharp.Web.Pages.Basket;

[Authorize]
public class CheckoutModel : PageModel
{
    private readonly IBasketService _basketService;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IOrderService _orderService;
    private string? _username = null;
    private readonly IBasketViewModelService _basketViewModelService;
    private readonly IAppLogger<CheckoutModel> _logger;

    public CheckoutModel(IBasketService basketService,
        IBasketViewModelService basketViewModelService,
        SignInManager<ApplicationUser> signInManager,
        IOrderService orderService,
        IAppLogger<CheckoutModel> logger)
    {
        _basketService = basketService;
        _signInManager = signInManager;
        _orderService = orderService;
        _basketViewModelService = basketViewModelService;
        _logger = logger;
    }

    public BasketViewModel DataContext { get; set; } = new();

    public async Task OnGet()
    {
        await SetBasketModelAsync();
    }

    public async Task<IActionResult> OnPost(IEnumerable<BasketItemViewModel> items)
    {
        try
        {
            await SetBasketModelAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var updateModel = items.ToDictionary(b => b.Id.ToString(), b => b.Quantity);
            await _basketService.SetQuantities(DataContext.Id, updateModel);
            await _orderService.CreateOrderAsync(DataContext.Id, new Address()
            {
                City = "Mohammedia",
                Country = "Morocco",
                State = "Casablanca",
                Street = "123 Main St.",
                ZipCode = "44240"
            });
            await _basketService.DeleteBasketAsync(DataContext.Id);
        }
        catch (EmptyBasketOnCheckoutException exception)
        {
            //Redirect to Empty Basket page
            _logger.LogWarning(exception.Message);
            return RedirectToPage("/Basket/Index");
        }

        return RedirectToPage("Success");
    }

    private async Task SetBasketModelAsync()
    {
        Debug.Assert(!string.IsNullOrEmpty(User?.Identity?.Name));
        if (_signInManager.IsSignedIn(HttpContext.User))
        {
            DataContext = await _basketViewModelService.GetOrCreateBasketForUser(User.Identity.Name);
        }
        else
        {
            GetOrSetBasketCookieAndUserName();
            DataContext = await _basketViewModelService.GetOrCreateBasketForUser(_username!);
        }
    }

    private void GetOrSetBasketCookieAndUserName()
    {
        Request.Cookies.TryGetValue(BasketConstants.BasketCookieName, out _username);
        if (_username != null)
            return;

        _username = Guid.NewGuid().ToString();
        var cookieOptions = new CookieOptions
        {
            Expires = DateTime.Today.AddYears(10)
        };
        Response.Cookies.Append(BasketConstants.BasketCookieName, _username, cookieOptions);
    }
}
