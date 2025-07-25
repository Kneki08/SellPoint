using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Aplication.Dtos.DetallePedido
{
    public record SaveDetallePedidoDTO
    {
        public int PedidoId { get; init; }
        public int ProductoId { get; init; }
        public int Cantidad { get; init; }
        public decimal PrecioUnitario { get; init; }
       
    }
}
