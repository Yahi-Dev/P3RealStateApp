using System.ComponentModel.DataAnnotations;

namespace RealStateApp.Core.Application.ViewModels.Domain.Improvement
{
    public class SaveImprovementViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The improvement type is required")]
        [DataType(DataType.Text)]
        public string ImprovementType { get; set; }

        [Required(ErrorMessage = "The description is required")]
        [DataType(DataType.Text)]
        public string Description { get; set; }
    }
}
