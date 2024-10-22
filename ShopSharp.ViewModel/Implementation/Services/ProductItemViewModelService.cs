using System.Diagnostics;
using ShopSharp.Infrastructure.EF;
using ShopSharp.Infrastructure.Entity;
using ShopSharp.ViewModel.Services;
using ShopSharp.ViewModel.Vm;

namespace ShopSharp.ViewModel.Implementation.Services;

internal class ProductItemViewModelService(
    IRepository<ProductItem> _productItemRepository) : IProductItemViewModelService
{
    public async Task UpdateCatalogItem(ProductItemViewModel viewModel)
    {
        var existingCatalogItem = await _productItemRepository.GetByIdAsync(viewModel.Id);
        Debug.Assert(existingCatalogItem != null);

        ProductItem.Details details = new()
        {
            Name = existingCatalogItem.Name,
            Description = existingCatalogItem.Description,
            Price = viewModel.Price
        };
        existingCatalogItem.UpdateDetails(details);
        await _productItemRepository.UpdateAsync(existingCatalogItem);
    }
}
