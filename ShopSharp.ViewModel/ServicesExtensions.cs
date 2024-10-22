using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ShopSharp.ViewModel.Implementation.Services;
using ShopSharp.ViewModel.Services;

namespace ShopSharp.ViewModel;

public static class ServicesExtensions
{
    public static IServiceCollection RegisterViewModelServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(BasketViewModelService).Assembly));
        services.AddScoped<IBasketViewModelService, BasketViewModelService>();
        services.AddScoped<ProductViewModelService>();
        services.AddScoped<IProductItemViewModelService, ProductItemViewModelService>();
        services.AddScoped<IProductViewModelService, ProductViewModelService>();
        return services;
    }
}
