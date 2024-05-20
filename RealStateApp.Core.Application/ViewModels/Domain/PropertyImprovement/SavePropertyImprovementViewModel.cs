using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModels.Domain.PropertyImprovement
{
    public class SavePropertyImprovementViewModel
    {
        public int? Id { get; set; }
        public int PropertyId { get; set; }
        public int ImprovementId { get; set; }
    }
}
