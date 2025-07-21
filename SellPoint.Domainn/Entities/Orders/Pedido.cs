using SellPoint.Domain.Entities.Orders;
using SellPoint.Domain.Entities.Users;
using SellPoint.Domainn.Base;
using SellPoint.Domainn.Entities.Products;
using SellPoint.Domainn.Entities.Users;

namespace SellPoint.Domainn.Entities.Orders
{
    public class Pedido : AuditEntity
    {
        public int IdUsuario { get; set; }
        public string NumeroPedido { get; set; } = string.Empty;

        public decimal Subtotal { get; set; }
        public decimal Descuento { get; set; } = 0m;
        public decimal CostoEnvio { get; set; }
        public decimal Total { get; set; }

        public EstadoPedido Estado { get; set; } = EstadoPedido.EnPreparacion;
        public MetodoPago MetodoPago { get; set; }

        public string? ReferenciaPago { get; set; }

        public int? CuponId { get; set; }
        public int? DireccionEnvioId { get; set; }

        public string? Notas { get; set; }

        // Relaciones de navegación
        public virtual Cliente? Cliente { get; set; }
        public virtual Cupon? Cupon { get; set; }
        public virtual DireccionEnvio? DireccionEnvio { get; set; }
        public virtual ICollection<DetallePedido> Detalles { get; set; } = new List<DetallePedido>();
    }

    public enum EstadoPedido
    {
        EnPreparacion,
        Enviado,
        Entregado,
        Cancelado
    }

    public enum MetodoPago
    {
        PayPal,
        TransferenciaBancaria,
        Tarjeta
    }
}
