using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RealStateApp.Core.Application.ViewModels.Identity.Users
{
    public class SaveDeveloperViewModel
    {
        public string? Id { get; set; }
        [Required(ErrorMessage = "Debe colocar el nombre del usuario")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }




        [Required(ErrorMessage = "Debe colocar el apellido del usuario")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }



        public string? ImageUrl { get; set; }




        [Required(ErrorMessage = "Debe colocar el nombre del usuario")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }




        [Required(ErrorMessage = "Debe colocar un correo")]
        [DataType(DataType.Text)]
        public string Email { get; set; }




        [Required(ErrorMessage = "Debe colocar una contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }





        [Compare(nameof(Password), ErrorMessage = "Las Contraseñas deben coincidir")]
        [Required(ErrorMessage = "Debe colocar el nombre del usuario")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }





        [Required(ErrorMessage = "Debe seleccionar un Role")]
        [DataType(DataType.Text)]
        public string Role { get; set; }
        public bool HasError {  get; set; }
        public string Error {  get; set; }


        [DataType(DataType.Upload)]
        public IFormFile? formFile { get; set; }
    }
}
