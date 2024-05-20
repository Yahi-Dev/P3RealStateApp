using RealStateApp.Core.Application.Interfaces.Services.Base;
using RealStateApp.Core.Application.ViewModels.Domain.Improvement;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Interfaces.Services.Domain
{
    public interface IImprovementService : IGenericService<SaveImprovementViewModel, BaseImprovementViewModel, Improvement>
    {

    }
}
