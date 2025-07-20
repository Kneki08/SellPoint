using System;

namespace SellPoint.Aplication.Dtos.Pedido
{
    public class SavePedidoDTO : BasePedidoDTO
    {
        public decimal Subtotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal CostoEnvio { get; set; }
        public decimal Total { get; set; }

        public string Notas { get; set; } = string.Empty;
    }
}