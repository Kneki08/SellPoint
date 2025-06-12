

using SellPoint.Domainn.Base;

namespace SellPoint.Domainn.Entities.Products
{
    public sealed class Catergoria : AuditiEntity
    {

        public string nombre { get; set; } = string.Empty;

        public string descripcion { get; set; } = string.Empty;

        public DateTime fechaCreacion { get; set; } = DateTime.UtcNow;

        public DateTime fechaActualizacion { get; set; } = DateTime.UtcNow;
    }
}
