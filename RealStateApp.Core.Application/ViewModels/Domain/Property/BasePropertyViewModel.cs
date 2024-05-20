using RealStateApp.Core.Application.Dtos.Domain_Dtos;
using RealStateApp.Core.Application.ViewModels.Domain.Improvement;
using RealStateApp.Core.Application.ViewModels.Domain.PropertyImage;
using RealStateApp.Core.Application.ViewModels.Domain.PropertyImprovement;
using RealStateApp.Core.Application.ViewModels.Domain.PropertyType;
using RealStateApp.Core.Application.ViewModels.Domain.SaleCategory;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.ViewModels.Domain.Property
{
    public class BasePropertyViewModel
    {
        public int Id { get; set; }
        public int PropertyTypeId { get; set; }
        public string Image {  get; set; }
        public string Code { get; set; }
        public int SaleCategoryId { get; set; }
        public string Location { get; set; }
        public decimal Price { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public float Size { get; set; }
        public string AgentId { get; set; }
        public DateTime CreatedDate { get; set; }
        public BasePropertyTypeViewModel PropertyType { get; set; }
        public List<Core.Domain.Entities.PropertyImage> Images { get; set; }
        public BaseSaleCategoryViewModel SaleCategory { get; set; }
        public List<BasePropertyImprovementViewModel>? Improvements { get; set; }
    }
}
