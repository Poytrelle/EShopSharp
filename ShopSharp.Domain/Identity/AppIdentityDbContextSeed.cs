using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopSharp.Infrastructure.Constants;

namespace ShopSharp.Domain.Identity;

public class AppIdentityDbContextSeed
{
    public static async Task SeedAsync(AppIdentityDbContext identityDbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {

        if (identityDbContext.Database.IsSqlServer())
        {
            identityDbContext.Database.Migrate();
        }

        await roleManager.CreateAsync(new IdentityRole(RoleConstants.Administrators));

        var defaultUser = new ApplicationUser { UserName = "px@px.com", Email = "px@px.com" };
        await userManager.CreateAsync(defaultUser, AuthorizationConstants.DefaultPassword);

        string adminUserName = "px@px.com";
        var adminUser = new ApplicationUser { UserName = adminUserName, Email = adminUserName };
        await userManager.CreateAsync(adminUser, AuthorizationConstants.DefaultPassword);
        adminUser = await userManager.FindByNameAsync(adminUserName);
        if (adminUser != null)
        {
            await userManager.AddToRoleAsync(adminUser, RoleConstants.Administrators);
        }
    }
}
