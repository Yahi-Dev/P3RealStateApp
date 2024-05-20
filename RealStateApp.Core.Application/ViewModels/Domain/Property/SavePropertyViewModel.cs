using Microsoft.AspNetCore.Http;
using RealStateApp.Core.Application.Dtos.Domain_Dtos;
using RealStateApp.Core.Application.ViewModels.Domain.Improvement;
using RealStateApp.Core.Application.ViewModels.Domain.PropertyImprovement;
using RealStateApp.Core.Application.ViewModels.Domain.PropertyType;
using RealStateApp.Core.Application.ViewModels.Domain.SaleCategory;
using System.ComponentModel.DataAnnotations;

namespace RealStateApp.Core.Application.ViewModels.Domain.Property
{
    public class SavePropertyViewModel
    {
        public int Id { get; set; }
        public string AgentId { get; set; }
        public string? Code { get; set; }


        [Required(ErrorMessage = "Debe colocar el tipo de la propiedad")]
        [DataType(DataType.Text)]
        public int PropertyTypeId { get; set; }
        public List<BasePropertyTypeViewModel>? PropertyTypes { get; set; }


        [Required(ErrorMessage = "Debe colocar el tipo de la venta")]
        [DataType(DataType.Text)]
        public int SaleCategoryId { get; set; }
        public List<BaseSaleCategoryViewModel>? SalesCategories { get; set; }



        [Required(ErrorMessage = "Debe colocar el valor que va a tener la propiedad")]
        [DataType(DataType.Text)]
        public decimal Price { get; set; }



        [Required(ErrorMessage = "La propiedad debe tener una descripcion")]
        [DataType(DataType.Text)]
        public string Description { get; set; }




        [Required(ErrorMessage = "Debe colocar el valor de la propiedad")]
        [DataType(DataType.Text)]
        public float Size { get; set; }




        [Required(ErrorMessage = "Debe colocar cuantas habitaciones tiene la propiedad")]
        [DataType(DataType.Text)]
        public int Bedrooms { get; set; }



        [Required(ErrorMessage = "Debe colocar la ubicación de la propiedad")]
        [DataType(DataType.Text)]
        public string Location { get; set; }




        [Required(ErrorMessage = "Debe colocar cuantas baños tiene la propiedad")]
        [DataType(DataType.Text)]
        public int Bathrooms { get; set; }


        [Required(ErrorMessage = "Debe colocar algunas mejoras que requiere la propiedad")]
        public List<int>? Improvement { get; set; }
        public List<BaseImprovementViewModel>? Improvements { get; set; }



        [DataType(DataType.Text)]
        public List<string>? ImagePath { get; set; }

        [Required(ErrorMessage = "Debe subir al menos una foto de su propiedad.")]

        [DataType(DataType.Upload)]
        public IFormFile? Image1 { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? Image2 { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? Image3 { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? Image4 { get; set; }
    }
}
