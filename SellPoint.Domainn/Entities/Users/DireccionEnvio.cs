using SellPoint.Domainn.Base;

namespace SellPoint.Domain.Entities.Users
{
    public sealed class DireccionEnvio : AuditEntity
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Usuario { get; set; }

        public string NombreCompleto { get; set; }
        public string DireccionLinea1 { get; set; }
        public string DireccionLinea2 { get; set; }
        public string Ciudad { get; set; }
        public string EstadoProvincia { get; set; }
        public string CodigoPostal { get; set; }
        public string Pais { get; set; } = "República Dominicana";
        public string Telefono { get; set; }

        public bool EsPrincipal { get; set; }
        public bool Activo { get; set; } = true;
    }
}
