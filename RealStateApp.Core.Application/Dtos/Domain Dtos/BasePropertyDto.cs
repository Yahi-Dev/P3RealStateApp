using RealStateApp.Core.Application.ViewModels.Domain.PropertyImprovement;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Dtos.Domain_Dtos
{
    public class BasePropertyDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int PropertyTypeId { get; set; }
        public BasePropertyTypeDto PropertyType { get; set; }
        public int SaleCategoryId { get; set; }
        public BaseSaleCategoryDto SaleCategory { get; set; }
        public decimal Price { get; set; }
        public float Size { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public string Description { get; set; }
        public string AgentId { get; set; }
        //public List<BaseImprovementDto> Improvements { get; set; }

        public List<BasePropertyImprovementViewModel> Improvements { get; set; }
        //public List<Improvement> Improvement { get; set; }

        public List<PropertyImage> Images { get; set; }
    }
}
