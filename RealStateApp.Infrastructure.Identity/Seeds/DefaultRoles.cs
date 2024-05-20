using Microsoft.AspNetCore.Identity;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Infrastructure.Identity.Entities;

namespace RealStateApp.Infrastructure.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(RolesEnum.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(RolesEnum.Developer.ToString()));
            await roleManager.CreateAsync(new IdentityRole(RolesEnum.Agent.ToString()));
            await roleManager.CreateAsync(new IdentityRole(RolesEnum.Client.ToString()));
        }
    }
}
