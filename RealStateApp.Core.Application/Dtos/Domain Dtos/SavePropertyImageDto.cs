namespace RealStateApp.Core.Application.Dtos.Domain_Dtos
{
    public class SavePropertyImageDto
    {
        public int Id { get; set; }
        public string ImageURL { get; set; }
        public bool IsMain { get; set; }
        public int PropertyId { get; set; }
    }
}
