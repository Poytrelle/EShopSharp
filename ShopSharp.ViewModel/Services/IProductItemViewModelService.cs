using ShopSharp.ViewModel.Vm;

namespace ShopSharp.ViewModel.Services;

public interface IProductItemViewModelService
{
    Task UpdateCatalogItem(ProductItemViewModel viewModel);
}
