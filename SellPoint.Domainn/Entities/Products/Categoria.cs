

using SellPoint.Domainn.Base;

namespace SellPoint.Domainn.Entities.Products
{
    public sealed class Categoria : AuditEntity
    {

        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        
    }
}
