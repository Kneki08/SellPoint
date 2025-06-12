using SellPoint.Domainn.Base;

namespace SellPoint.Domain.Entities.Users
{
    public sealed class Sesion : AuditEntity
    {
        public int UsuarioId { get; set; }

        public string TokenSesion { get; set; } = null!;

        public string? DireccionIP { get; set; }

        public string? UserAgent { get; set; }

        public DateTime FechaExpiracion { get; set; }

        public bool Activa { get; set; } = true;
    }
}
