using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Aplication.Dtos.Pedido
{
    public record SavePedidoDTO
    {
        public int UsuarioId { get; init; }
        public decimal Subtotal { get; init; }
        public decimal Descuento { get; init; }
        public decimal CostoEnvio { get; init; }
        public decimal Total { get; init; }
        public string? MetodoPago { get; init; }
        public string? ReferenciaPago { get; init; }
        public int? CuponId { get; init; }
        public int? DireccionEnvioId { get; init; }
        public string? Notas { get; init; }
    }
}
