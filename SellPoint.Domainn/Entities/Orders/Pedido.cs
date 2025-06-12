

using SellPoint.Domainn.Base;

namespace SellPoint.Domainn.Entities.Orders
{
    public class Pedido : AuditiEntity
    {
        public int UsuarioId { get; set; }
        public string NumeroPedido { get; set; } = string.Empty;

        public decimal Subtotal { get; set; }
        public decimal Descuento { get; set; } = 0m;
        public decimal CostoEnvio { get; set; }
        public decimal Total => Subtotal - Descuento + CostoEnvio;

        public EstadoPedido Estado { get; set; } = EstadoPedido.EnPreparacion;
        public MetodoPago MetodoPago { get; set; }

        public string? ReferenciaPago { get; set; }

        public int? CuponId { get; set; }
        public int? DireccionEnvioId { get; set; }

        public string? Notas { get; set; }

        public DateTime FechaPedido { get; set; } = DateTime.UtcNow;
        public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;

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
