using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModels.Domain.PropertyType
{
    public class SavePropertyTypeViewModel
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
