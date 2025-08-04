

namespace SellPoint.View.Models.ModelDetallePedido.Dtos
{
    public class DetalleDto 
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int PedidoId { get; set; }
        public int ProductoId { get; set; }

    }
}
