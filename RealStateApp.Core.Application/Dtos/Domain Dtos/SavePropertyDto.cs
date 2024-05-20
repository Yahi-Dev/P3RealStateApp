using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Dtos.Domain_Dtos
{
    public class SavePropertyDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int PropertyTypeId { get; set; }
        public int SaleCategoryId { get; set; }
        public decimal Price { get; set; }
        public float Size { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public string Description { get; set; }
        public string AgentId { get; set; }
    }
}
