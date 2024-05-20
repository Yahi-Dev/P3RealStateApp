namespace RealStateApp.Core.Application.Dtos.Domain_Dtos
{
    public class BaseClientFavoriteProperty
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public int PropertyId { get; set; }
        public Domain.Entities.Property Property { get; set; }
    }
}
