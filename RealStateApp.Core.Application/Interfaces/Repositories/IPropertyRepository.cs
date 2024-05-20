using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Interfaces.Repositories
{
    public interface IPropertyRepository : IGenericRepository<Property>
    {
        Task<List<PropertyImage>> GetPropertyImagesById(int propertyId);
        Task<List<Core.Domain.Entities.Property>> GetAllWithIncludeAsync(List<string> properties);
    }
}
