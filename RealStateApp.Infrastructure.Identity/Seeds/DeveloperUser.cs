using Microsoft.AspNetCore.Identity;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Infrastructure.Identity.Entities;

namespace RealStateApp.Infrastructure.Identity.Seeds
{
    public class DeveloperUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser developeruser = new();
            developeruser.UserName = "developeruser";
            developeruser.Email = "developeruser@email.com";
            developeruser.FirstName = "Developer";
            developeruser.LastName = "User";
            developeruser.PhoneNumber = "829-123-9811";
            developeruser.IdCard = "40283599";
            developeruser.ImageUrl = "";
            developeruser.IsActive = true;
            developeruser.EmailConfirmed = true;
            developeruser.PhoneNumberConfirmed = true;

            if (userManager.Users.All(u => u.Id != developeruser.Id))
            {
                var user = await userManager.FindByEmailAsync(developeruser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(developeruser, "123Pa$$word");
                    await userManager.AddToRoleAsync(developeruser, RolesEnum.Developer.ToString());
                }
            }
        }
    }
}
