using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopSharp.Infrastructure.Entity;

namespace ShopSharp.Domain.Data;

public class CatalogContextSeed
{
    public static async Task SeedAsync(CatalogContext catalogContext, ILogger logger, int retry = 0)
    {
        var retryForAvailability = retry;
        try
        {
            if (catalogContext.Database.IsSqlServer())
            {
                catalogContext.Database.Migrate();
            }

            if (!await catalogContext.ProductBrands.AnyAsync())
            {
                await catalogContext.ProductBrands.AddRangeAsync(
                    GetPreconfiguredProductBrands());

                await catalogContext.SaveChangesAsync();
            }

            if (!await catalogContext.ProductTypes.AnyAsync())
            {
                await catalogContext.ProductTypes.AddRangeAsync(
                    GetPreconfiguredProductTypes());

                await catalogContext.SaveChangesAsync();
            }

            if (!await catalogContext.ProductItems.AnyAsync())
            {
                await catalogContext.ProductItems.AddRangeAsync(
                    GetPreconfiguredItems(
                        catalogContext.ProductTypes,
                        catalogContext.ProductBrands));

                await catalogContext.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            if (retryForAvailability >= 10)
                throw;

            retryForAvailability++;

            logger.LogError(ex.Message);
            await SeedAsync(catalogContext, logger, retryForAvailability);
            throw;
        }
    }

    static IEnumerable<ProductBrand> GetPreconfiguredProductBrands()
    {
        return
            [
                new(){ Name = "IPhone" },
                new(){ Name = "Samsung" },
                new(){ Name = "Huawei" },
                new(){ Name = "Xiaomi" },
                new(){ Name = "Sony" },
                new(){ Name = "LG" }
            ];
    }

    static IEnumerable<ProductType> GetPreconfiguredProductTypes()
    {
        return
            [
                new(){ Name = "Smartphone" },
                new(){ Name = "Tablet" },
                new(){ Name = "Laptop" },
                new(){ Name = "Desktop" },
                new(){ Name = "TV" },
                new(){ Name = "Smartwatch" }
            ];
    }

    static IEnumerable<ProductItem> GetPreconfiguredItems(
        DbSet<ProductType> catalogTypes,
        DbSet<ProductBrand> catalogBrands)
    {
        var iphoneId = catalogBrands.Single(c => c.Name == "IPhone").Id;
        var samsungId = catalogBrands.Single(c => c.Name == "Samsung").Id;
        var huaweiId = catalogBrands.Single(c => c.Name == "Huawei").Id;
        var xiaomiId = catalogBrands.Single(c => c.Name == "Xiaomi").Id;

        var smartphoneId = catalogTypes.Single(c => c.Name == "Smartphone").Id;

        return
            [
                new() { TypeId = smartphoneId, BrandId = iphoneId, Name = "IPhone 12",
                        Description = "IPhone 12", Price = 799.99M,  PictureUri = "http://<BASE_URL>/images/products/apple/iphone_12.png" },
                new() { TypeId = smartphoneId, BrandId = iphoneId, Name = "IPhone 12 Pro",
                        Description = "IPhone 12 Pro", Price = 999.99M, PictureUri = "http://<BASE_URL>/images/products/apple/iphone_12_pro.png" },
                new() { TypeId = smartphoneId, BrandId = iphoneId, Name = "IPhone 12 Pro Max",
                        Description = "IPhone 12 Pro Max", Price = 1099.99M, PictureUri = "http://<BASE_URL>/images/products/apple/iphone_12_pro_max.png" },

                new() { TypeId = smartphoneId,BrandId = samsungId, Name = "Galaxy S21",
                        Description = "Galaxy S21", Price = 799.99M, PictureUri = "http://<BASE_URL>/images/products/samsung/s21.png" },
                new() { TypeId = smartphoneId,BrandId = samsungId, Name = "Galaxy S21 Plus",
                        Description = "Galaxy S21 Plus", Price = 999.99M, PictureUri = "http://<BASE_URL>/images/products/samsung/s21_plus.png" },
                new() { TypeId = smartphoneId,BrandId = samsungId, Name = "Galaxy S21 Ultra",
                        Description = "Galaxy S21 Ultra", Price = 1199.99M, PictureUri = "http://<BASE_URL>/images/products/samsung/s21_ultra.png" },

                new() { TypeId = smartphoneId, BrandId = huaweiId, Name = "MateBook D 15",
                        Description = "MateBook D 15", Price = 599.99M, PictureUri = "http://<BASE_URL>/images/products/huawei/matebook_d_15.png" },
                new() { TypeId = smartphoneId, BrandId = huaweiId, Name = "MateBook X Pro",
                        Description = "MateBook X Pro", Price = 1199.99M, PictureUri = "http://<BASE_URL>/images/products/huawei/matebook_x_pro.png" },
                new() { TypeId = smartphoneId, BrandId = huaweiId, Name = "MateBook 14",
                        Description = "MateBook 14", Price = 899.99M, PictureUri = "http://<BASE_URL>/images/products/huawei/matebook_14.png" },

                new() { TypeId = smartphoneId, BrandId = xiaomiId, Name = "Mi 11",
                        Description = "Mi 11", Price = 799.99M, PictureUri = "http://<BASE_URL>/images/products/xiaomi/mi_11.png" },
                new() { TypeId = smartphoneId, BrandId = xiaomiId, Name = "Mi 11 Lite",
                        Description = "Mi 11 Lite", Price = 399.99M, PictureUri = "http://<BASE_URL>/images/products/xiaomi/mi_11_lite.png" },
                new() { TypeId = smartphoneId, BrandId = xiaomiId, Name = "Mi 11 Ultra",
                        Description = "Mi 11 Ultra", Price = 1199.99M, PictureUri = "http://<BASE_URL>/images/products/xiaomi/mi_11_ultra.png"  }
            ];
    }
}
