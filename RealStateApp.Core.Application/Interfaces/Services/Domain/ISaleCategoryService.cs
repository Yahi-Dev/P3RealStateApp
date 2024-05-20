using RealStateApp.Core.Application.Interfaces.Services.Base;
using RealStateApp.Core.Application.ViewModels.Domain.SaleCategory;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Interfaces.Services.Domain
{
    public interface ISaleCategoryService : IGenericService<SaveSaleCategoryViewModel, BaseSaleCategoryViewModel, SaleCategory>
    {

    }
}
