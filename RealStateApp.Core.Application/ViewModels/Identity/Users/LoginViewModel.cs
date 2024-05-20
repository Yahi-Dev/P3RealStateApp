using System.ComponentModel.DataAnnotations;

namespace RealStateApp.Core.Application.ViewModels.Identity.Users
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Debe colocar el nombre del usuario")]
        [DataType(DataType.Text)]
        public string Email { get; set; }



        [Required(ErrorMessage = "Debe colocar una contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        public string? Error { get; set; }
        public bool HasError { get; set; }
    }
}
