using RealStateApp.Core.Application.Interfaces.Services.Base;
using RealStateApp.Core.Application.ViewModels.Domain.PropertyImprovement;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Interfaces.Services.Domain
{
    public interface IPropertyImprovementService : IGenericService<SavePropertyImprovementViewModel, BasePropertyImprovementViewModel, PropertyImprovement>
    {
    }
}
