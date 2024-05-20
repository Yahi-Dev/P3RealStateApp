using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace RealStateApp.Core.Application.Dtos.Account
{
    public class RegisterRequest
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string IdCard { get; set; }
        [JsonIgnore]
        public IFormFile? formFile { get; set; }
        [JsonIgnore]
        public string? ImageUrl { get; set; }
        [JsonIgnore]
        public bool IsActive { get; set; }
        public string PhoneNumber { get; set; }
    }
}
