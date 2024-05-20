using RealStateApp.Core.Application.ViewModels.Domain.Property;

namespace RealStateApp.Core.Application.ViewModels.Domain.PropertyImage
{
    public class BasePropertyImageViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string ImageURL { get; set; }
        public bool IsMain { get; set; }
        public int PropertyId { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
