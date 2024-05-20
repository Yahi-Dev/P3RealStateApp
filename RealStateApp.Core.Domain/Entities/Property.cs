namespace RealStateApp.Core.Domain.Entities
{
    public class Property
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int PropertyTypeId { get; set; }
        public PropertyType PropertyType { get; set; }
        public int SaleCategoryId { get; set; }
        public SaleCategory SaleCategory { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public float Size { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public string Description { get; set; }
        public string AgentId { get; set; }
        public ICollection<PropertyImprovement>? Improvements { get; set; }
        public ICollection<PropertyImage>? Images { get; set; }
        public string? CreatedById { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? LastModifiedById { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool? Deleted { get; set; }
        public string? DeletedById { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
