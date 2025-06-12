

using SellPoint.Domainn.Base;

namespace SellPoint.Domain.Entities.Orders
{
    public sealed class DetallePedido : AuditEntity
    {

        public int Pedidoid { get; set; } 
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal => Cantidad * PrecioUnitario;

        public DateTime Fechaagregado { get; set; } = DateTime.UtcNow;


    }
}
