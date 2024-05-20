using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModels.Domain.PropertyType
{
    public class BasePropertyTypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PropertiesCount { get; set; }
    }
}
