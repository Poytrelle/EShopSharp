using ShopSharp.Infrastructure.Services;
using ShopSharp.ViewModel;

namespace ShopSharp.Web.Config;

public static class ConfigureWebServices
{
    public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CatalogSettings>(configuration);
        services.RegisterViewModelServices();

        return services;
    }
}
