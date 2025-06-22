using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Aplication.Dtos.Carrito
{
    public record  SaveCarritoDTO
    {
        public int UsuarioId { get; init; }
        public int ProductoId { get; init; }
        public int Cantidad { get; init; }
    }
}
