using SellPoint.Domainn.Base;

namespace SellPoint.Domain.Entities.Users
{
    public sealed class Sesion : AuditiEntity
    {
      public int UsuarioId { get; set; }
      public DateTime FechaInicioSesion { get; set; }
      public DateTime? FechaCierreSesion { get; set; }
    }
}
