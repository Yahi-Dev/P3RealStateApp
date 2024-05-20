using RealStateApp.Core.Application.ViewModels.Domain.Improvement;

namespace RealStateApp.Core.Application.ViewModels.Domain.PropertyImprovement
{
    public class BasePropertyImprovementViewModel
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public BasePropertyImprovementViewModel Property { get; set; }
        public int ImprovementId { get; set; }
        public BaseImprovementViewModel Improvement { get; set; }
    }
}
