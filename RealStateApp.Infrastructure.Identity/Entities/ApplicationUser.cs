using Microsoft.AspNetCore.Identity;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Infrastructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? ImageUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdCard { get; set; }
        public bool IsActive { get; set; }
    }
}
