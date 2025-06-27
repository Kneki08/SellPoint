using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Aplication.Dtos.Pedido
{
    public record PedidoDTO
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal CostoEnvio { get; set; }
        public decimal Total { get; set; }
        public string? MetodoPago { get; set; }
        public string? ReferenciaPago { get; set; }
        public int? CuponId { get; set; }
        public int? DireccionEnvioId { get; set; }
        public string? Notas { get; set; }
        public string? Estado { get; set; }
    }
}
