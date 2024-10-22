using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ShopSharp.Domain.Data;
using ShopSharp.Domain.Implementation.Services;
using ShopSharp.Infrastructure.EF;
using ShopSharp.Infrastructure.Services;

namespace ShopSharp.Web.Config;

public static class ConfigureCoreServices
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

        // if UseBasketDB is true, use the database basket service
        if (configuration.GetValue<bool>("UseBasketDB", false))
        {
            services.AddScoped<IBasketService, DBBasketService>();
            services.AddScoped<IBasketQueryService, DBBasketQueryService>();
        }
        else
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddScoped<IBasketService, CachedBasketService>();
            services.AddScoped<IBasketQueryService, CachedBasketQueryService>();
        }

        var catalogSettings = configuration.Get<CatalogSettings>() ?? new CatalogSettings();
        services.AddSingleton<IUriComposer>(new UriComposer(catalogSettings));

        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
        services.AddTransient<IEmailSender, EmailSender>();

        return services;
    }
}
