namespace SellPoint.View.Dtos.Pedido
{
    /// <summary>
    /// Contiene los datos base comunes a todos los pedidos.
    /// </summary>
    public abstract class BasePedidoDTO
    {
        // Datos generales
        public required int IdUsuario { get; set; }
        public required DateTime FechaPedido { get; set; }
        public required string Estado { get; set; }
        public required int IdDireccionEnvio { get; set; }

        // Referencias externas
        public required int? CuponId { get; set; }

        // Datos de pago
        public required string MetodoPago { get; set; }
        public required string ReferenciaPago { get; set; }

        // Montos
        public decimal Subtotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal CostoEnvio { get; set; }
        public decimal Total { get; set; }
    }
}