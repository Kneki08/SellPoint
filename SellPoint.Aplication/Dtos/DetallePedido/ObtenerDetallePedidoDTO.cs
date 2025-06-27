using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Aplication.Dtos.DetallePedido
{
    public record ObtenerDetallePedidoDTO
    {
        public int ProductoId { get; init; }
        public string NombreProducto { get; init; } = string.Empty;
        public int Cantidad { get; init; }
        public decimal PrecioUnitario { get; init; }
        public decimal Subtotal { get; init; }
    }
}
