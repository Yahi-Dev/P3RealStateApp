namespace RealStateApp.Core.Domain.Entities
{
    public class PropertyType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Property> Properties { get; set; }
        public string? CreatedById { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? LastModifiedById { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool? Deleted { get; set; }
        public string? DeletedById { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
