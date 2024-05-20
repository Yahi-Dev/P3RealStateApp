namespace RealStateApp.Core.Application.ViewModels.Identity.Admin
{
    public class AgentViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int QuantityProperty { get; set; }
        public bool IsActive { get; set; }
        public string Role { get; set; }
    }
}
