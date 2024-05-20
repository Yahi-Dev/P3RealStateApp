using Microsoft.AspNetCore.Identity;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Infrastructure.Identity.Entities;

namespace RealStateApp.Infrastructure.Identity.Seeds
{
    public class ClientUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser clientuser = new();
            clientuser.UserName = "clientuser";
            clientuser.Email = "clientuser@email.com";
            clientuser.FirstName = "Client";
            clientuser.LastName = "User";
            clientuser.PhoneNumber = "829-123-9811";
            clientuser.IdCard = "40283598593";
            clientuser.ImageUrl = "";
            clientuser.IsActive = true;
            clientuser.EmailConfirmed = true;
            clientuser.PhoneNumberConfirmed = true;

            if (userManager.Users.All(u => u.Id != clientuser.Id))
            {
                var user = await userManager.FindByEmailAsync(clientuser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(clientuser, "123Pa$$word");
                    await userManager.AddToRoleAsync(clientuser, RolesEnum.Client.ToString());
                }
            }
        }
    }
}
