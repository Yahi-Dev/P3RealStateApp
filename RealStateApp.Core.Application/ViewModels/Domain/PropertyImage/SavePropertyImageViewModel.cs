namespace RealStateApp.Core.Application.ViewModels.Domain.PropertyImage
{
    public class SavePropertyImageViewModel
    {
        public int id { get; set; }
        public string ImageURL { get; set; }
        public bool IsMain { get; set; }
        public string UserId { get; set; }
        public int PropertyId { get; set; }
    }
}
