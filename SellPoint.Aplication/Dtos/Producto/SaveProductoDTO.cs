using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Aplication.Dtos.Producto
{
    public record SaveProductoDTO 
    {
        public string? Nombre { get; init; }
        public string? Descripcion { get; init; }
        public decimal Precio { get; init; }
        public int Stock { get; init; }
        public int? CategoriaId { get; init; }
        public string ImagenUri { get; init; }
        public bool Activo { get; init; }
    }
}
