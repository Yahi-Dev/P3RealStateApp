using Microsoft.AspNetCore.Identity;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Infrastructure.Identity.Entities;

namespace RealStateApp.Infrastructure.Identity.Seeds
{
    public class AdminUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser adminuser = new();
            adminuser.UserName = "adminuser";
            adminuser.Email = "adminuser@email.com";
            adminuser.FirstName = "admin";
            adminuser.LastName = "user";
            adminuser.PhoneNumber = "829-123-9811";
            adminuser.IdCard = "4028359859";
            adminuser.ImageUrl = "";
            adminuser.IsActive = true;
            adminuser.EmailConfirmed = true;
            adminuser.PhoneNumberConfirmed = true;

            if (userManager.Users.All(u => u.Id != adminuser.Id))
            {
                var user = await userManager.FindByEmailAsync(adminuser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(adminuser, "123Pa$$word");
                    await userManager.AddToRoleAsync(adminuser, RolesEnum.Admin.ToString());
                }
            }
        }
    }
}
