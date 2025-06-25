

namespace SellPoint.Aplication.Dtos.DetallePedido
{
    public record UpdateDetallePedidoDTO
    {
        public int Id { get; init; }
        public int Cantidad { get; init; }
        public decimal PrecioUnitario { get; init; }
        public decimal Subtotal { get; init; }
        public int PedidoId { get; set; }
        public int ProductoId { get; set; }
    }
}
