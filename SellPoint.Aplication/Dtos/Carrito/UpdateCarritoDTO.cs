using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Aplication.Dtos.Carrito
{
    public record UpdateCarritoDTO
    {
        public int UsuarioId { get; init; }
        public int ProductoId { get; init; }
        public int NuevaCantidad { get; init; }
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Estado { get; set; }
    }
}
