using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Aplication.Dtos.Pedido
{
    public record ObtenerPedidoDTO
    {
        public int Id { get; init; }
        public string NumeroPedido { get; init; } = string.Empty;
        public DateTime FechaPedido { get; init; }
        public decimal Total { get; init; }
        public string Estado { get; init; } = string.Empty;
        public int UsuarioId { get; init; }
    }
}
