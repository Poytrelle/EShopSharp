using Ardalis.Specification;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using ShopSharp.Infrastructure.EF;
using ShopSharp.Infrastructure.Entity;
using ShopSharp.Infrastructure.Services;
using ShopSharp.Infrastructure.Specifications;
using ShopSharp.ViewModel.Services;
using ShopSharp.ViewModel.Vm;

namespace ShopSharp.ViewModel.Implementation.Services;

internal class ProductViewModelService(
    ILoggerFactory _loggerFactory,
    IRepository<ProductItem> _itemRepository,
    IRepository<ProductBrand> _brandRepository,
    IRepository<ProductType> _typeRepository,
    IUriComposer _uriComposer) : IProductViewModelService
{
    private readonly ILogger<ProductViewModelService> _logger = _loggerFactory.CreateLogger<ProductViewModelService>();

    public async Task<IEnumerable<SelectListItem>> GetBrands()
    {
        _logger.LogInformation("GetBrands called.");

        var list = await _brandRepository.ListAsync();
        var items = list
            .Select(brand => new SelectListItem(brand.Name, brand.Id.ToString()))
            .OrderBy(b => b.Text)
            .ToList();

        items.Insert(0, new SelectListItem("All", null, true));
        return items;
    }

    public async Task<ProductViewModel> GetCatalogItems(int pageIndex, int itemsPage, int? brandId, int? typeId)
    {
        _logger.LogInformation("GetCatalogItems called.");

        var filterSpecification = new ProductFilterSpecification(brandId, typeId);
        var filterPaginatedSpecification =
            new ProductFilterPaginatedSpecification(itemsPage * pageIndex, itemsPage, brandId, typeId);

        // the implementation below using ForEach and Count. We need a List.
        var itemsOnPage = await _itemRepository.ListAsync(filterPaginatedSpecification);
        var totalItems = await _itemRepository.CountAsync(filterSpecification);

        var vm = new ProductViewModel()
        {
            Items = itemsOnPage.Select(i => new ProductItemViewModel()
            {
                Id = i.Id,
                Name = i.Name,
                PictureUri = _uriComposer.ComposePicUri(i.PictureUri),
                Price = i.Price
            }).ToList(),
            Brands = (await GetBrands()).ToList(),
            Types = (await GetTypes()).ToList(),
            BrandFilterApplied = brandId ?? 0,
            TypesFilterApplied = typeId ?? 0,
            PaginationInfo = new PaginationInfoViewModel()
            {
                ActualPage = pageIndex,
                ItemsPerPage = itemsOnPage.Count,
                TotalItems = totalItems,
                TotalPages = int.Parse(Math.Ceiling(((decimal)totalItems / itemsPage)).ToString())
            }
        };

        vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
        vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";

        return vm;
    }

    public async Task<IEnumerable<SelectListItem>> GetTypes()
    {
        _logger.LogInformation("GetTypes called.");

        var list = await _typeRepository.ListAsync();
        var items = list
            .Select(type => new SelectListItem(type.Name, type.Id.ToString()))
            .OrderBy(b => b.Text)
            .ToList();

        items.Insert(0, new SelectListItem("All", null, true));
        return items;
    }
}
