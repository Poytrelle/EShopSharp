using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShopSharp.ViewModel.Vm;

public class ProductViewModel
{
    public List<ProductItemViewModel> Items { get; set; } = [];
    public List<SelectListItem>? Brands { get; set; } = [];
    public List<SelectListItem>? Types { get; set; } = [];
    public int? BrandFilterApplied { get; set; }
    public int? TypesFilterApplied { get; set; }
    public PaginationInfoViewModel? PaginationInfo { get; set; }
}
