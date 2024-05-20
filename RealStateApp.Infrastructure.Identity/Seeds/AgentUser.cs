using Microsoft.AspNetCore.Identity;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Infrastructure.Identity.Entities;

namespace RealStateApp.Infrastructure.Identity.Seeds
{
    public class AgentUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser agentuser = new();
            agentuser.UserName = "agentuser";
            agentuser.Email = "agentuser@email.com";
            agentuser.FirstName = "Agent";
            agentuser.LastName = "User";
            agentuser.PhoneNumber = "829-123-9811";
            agentuser.IdCard = "40283598459";
            agentuser.ImageUrl = "";
            agentuser.IsActive = true;
            agentuser.EmailConfirmed = true;
            agentuser.PhoneNumberConfirmed = true;

            if (userManager.Users.All(u => u.Id != agentuser.Id))
            {
                var user = await userManager.FindByEmailAsync(agentuser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(agentuser, "123Pa$$word");
                    await userManager.AddToRoleAsync(agentuser, RolesEnum.Agent.ToString());
                }
            }
        }
    }
}
