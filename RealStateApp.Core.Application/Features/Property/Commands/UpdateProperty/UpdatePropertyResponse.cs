using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Property.Commands.UpdateProperty
{
    public class UpdatePropertyResponse
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int PropertyTypeId { get; set; }
        public int SaleCategoryId { get; set; }
        public decimal Price { get; set; }
        public float Size { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public string Description { get; set; }
        public string AgentId { get; set; }
    }
}
