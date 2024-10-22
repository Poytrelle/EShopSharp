using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopSharp.Domain.Data;
using ShopSharp.Domain.Identity;

namespace ShopSharp.Domain;

public static class DependenciesExtension
{
    public static IServiceCollection ConfigureShopSharpDb(this IServiceCollection services, IConfiguration configuration)
    {
        _ = bool.TryParse(configuration["UseOnlyInMemoryDatabase"], out bool useOnlyInMemoryDatabase);

        if (useOnlyInMemoryDatabase)
        {
            services.AddDbContext<CatalogContext>(c =>
               c.UseInMemoryDatabase("Catalog"));

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseInMemoryDatabase("Identity"));
        }
        else
        {
            // use real database
            // Requires LocalDB which can be installed with SQL Server Express 2016
            // https://www.microsoft.com/en-us/download/details.aspx?id=54284
            services.AddDbContext<CatalogContext>(c =>
                c.UseSqlServer(configuration.GetConnectionString("CatalogConnection")));

            // Add Identity DbContext
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));
        }

        return services;
    }
}
