using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Aplication.Dtos.Producto
{
    public record ObtenerProductoDTO
    {
        public int Id { get; init; }
        public string Nombre { get; init; } = string.Empty;
        public decimal Precio { get; init; }
        public int Stock { get; init; }
        public bool Activo { get; init; }
    }
}
