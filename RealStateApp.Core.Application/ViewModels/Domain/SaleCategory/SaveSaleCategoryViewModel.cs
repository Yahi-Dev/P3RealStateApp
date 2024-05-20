using System.ComponentModel.DataAnnotations;

namespace RealStateApp.Core.Application.ViewModels.Domain.SaleCategory
{
    public class SaveSaleCategoryViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The name is required")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Required(ErrorMessage = "The description is required")]
        [DataType(DataType.Text)]
        public string Description { get; set; }
    }
}
