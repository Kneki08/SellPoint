namespace SellPoint.Aplication.Dtos.Pedido
{
    public abstract class BaseMontosPedidoDTO : BasePedidoDTO
    {
        public decimal Subtotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal CostoEnvio { get; set; }

        public decimal Total { get; set; }
    }
}