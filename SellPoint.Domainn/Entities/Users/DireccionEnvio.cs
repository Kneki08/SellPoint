using SellPoint.Domainn.Base;

namespace SellPoint.Domain.Entities.Users
{
    public sealed class DireccionEnvio : AuditiEntity
    {
       public string? Calle { get; set; }
       public string? Ciudad { get; set; }
    }
}
