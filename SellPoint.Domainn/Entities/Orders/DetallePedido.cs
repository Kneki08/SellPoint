using SellPoint.Domainn.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SellPoint.Domain.Entities.Orders
{
    [Table("detalles_pedido")]
    public sealed class DetallePedido : AuditEntity
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("pedido_id")]
        public int Pedidoid { get; set; }

        [Column("producto_id")]
        public int ProductoId { get; set; }

        [Column("cantidad")]
        public int Cantidad { get; set; }

        [Column("precio_unitario")]
        public decimal PrecioUnitario { get; set; }
        [Column("subtotal")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal Subtotal { get; private set; }

    }
}
