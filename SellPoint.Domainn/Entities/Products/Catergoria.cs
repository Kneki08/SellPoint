

using SellPoint.Domainn.Base;

namespace SellPoint.Domainn.Entities.Products
{
    public sealed class Catergoria : AuditiEntity
    {

        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;
    }
}
