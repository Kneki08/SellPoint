

using SellPoint.Domainn.Base;

namespace SellPoint.Domainn.Entities.Products
{
    public class Producto : AuditiEntity
    {
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; } = 0;

        public int? CategoriaId { get; set; }
        public string? ImagenUrl { get; set; }

        public bool Activo { get; set; } = true;

    }
}
