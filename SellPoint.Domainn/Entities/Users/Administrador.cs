

using SellPoint.Domainn.Base;

namespace SellPoint.Domainn.Entities.Users
{
    public sealed class Administrador: Usuario
    {
        public string? Departamento { get; set; }
        public List<string> Permisos { get; set; } = new();
    }
}
