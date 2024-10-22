using Microsoft.AspNetCore.Mvc.Rendering;
using ShopSharp.ViewModel.Vm;

namespace ShopSharp.ViewModel.Services;

public interface IProductViewModelService
{
    Task<ProductViewModel> GetCatalogItems(int pageIndex, int itemsPage, int? brandId, int? typeId);

    Task<IEnumerable<SelectListItem>> GetTypes();
    Task<IEnumerable<SelectListItem>> GetBrands();
}
