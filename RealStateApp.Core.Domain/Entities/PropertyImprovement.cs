namespace RealStateApp.Core.Domain.Entities
{
    public class PropertyImprovement
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public Property Property { get; set; }
        public int ImprovementId { get; set; }
        public Improvement Improvement { get; set; }
    }
}
