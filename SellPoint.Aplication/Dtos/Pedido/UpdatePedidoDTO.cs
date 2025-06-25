using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Aplication.Dtos.Pedido
{
    public record UpdatePedidoDTO
    {
        public int Id { get; init; }
        public string? Estado { get; init; }
        public string? MetodoPago { get; init; }
        public string? ReferenciaPago { get; init; }
        public string? Notas { get; init; }
        public DateTime FechaActualizacion { get; init; }
    }
}
