using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModels.Identity.Users
{
    public class BaseUserViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string IdCard { get; set; }
        public bool IsActive { get; set; }
        public string PhoneNumber { get; set; }
        public string ImageUrl { get; set; }
        public string Password { get; set; }
    }
}
