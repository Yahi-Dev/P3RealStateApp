using RealStateApp.Core.Application.ViewModels.Domain.PropertyImage;

namespace RealStateApp.Core.Application.ViewModels.Domain.Property
{
    public class FiltersPropertiesViewModel
    {
        public string? Code { get; set; }
        public int? PropertyTypeId { get; set; }
        public string? SaleCategory { get; set; }
        public decimal? PriceMin { get; set; }
        public decimal? PriceMax { get; set; }
        public int? Bathrooms { get; set; }
        public int? Bedrooms { get; set; }

    }
}
