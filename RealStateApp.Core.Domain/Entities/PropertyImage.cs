namespace RealStateApp.Core.Domain.Entities
{
    public class PropertyImage
    {
        public int Id { get; set; }
        public string ImageURL { get; set; }
        public bool IsMain { get; set; }
        public int PropertyId { get; set; }
        public Property Property { get; set; }
        public string? CreatedById { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? LastModifiedById { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool? Deleted { get; set; }
        public string? DeletedById { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
