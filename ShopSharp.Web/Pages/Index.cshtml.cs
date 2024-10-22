using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopSharp.ViewModel.Constants;
using ShopSharp.ViewModel.Services;
using ShopSharp.ViewModel.Vm;

namespace ShopSharp.Web.Pages;

public class IndexModel : PageModel
{
    private readonly IProductViewModelService _productViewModelService;

    public IndexModel(IProductViewModelService productViewModelService)
    {
        _productViewModelService = productViewModelService;
    }

    public required ProductViewModel DataContext { get; set; } = new();

    public async Task OnGet(ProductViewModel dataContext, int? pageId)
    {
        DataContext = await _productViewModelService.GetCatalogItems(pageId ?? 0, ProductConstants.ItemsPerPage,
            dataContext.BrandFilterApplied, dataContext.TypesFilterApplied);
    }
}
