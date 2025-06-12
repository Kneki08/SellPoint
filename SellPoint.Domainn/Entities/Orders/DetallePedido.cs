

using SellPoint.Domainn.Base;

namespace SellPoint.Domain.Entities.Orders
{
    public sealed class DetallePedido : AuditiEntity
    {

        public int pedidoid { get; set; } 
        public int productoId { get; set; }
        public int cantidad { get; set; }
        public decimal precioUnitario { get; set; }
        public decimal subtotal { get; set; }

        public DateTime fechaagregado { get; set; } = DateTime.UtcNow;


    }
}
