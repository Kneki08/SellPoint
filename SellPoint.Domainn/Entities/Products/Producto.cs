

using SellPoint.Domainn.Base;

namespace SellPoint.Domainn.Entities.Products
{
    public class Producto : AuditiEntity
    {
        public string Nombre {  get; set; }
        public string? Descripcion { set; get; }
        public decimal Precio {get; set; }
        public int Stock {get; set; }
        
    }
}
