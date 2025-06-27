using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Aplication.Dtos.Producto
{
    public record ProductoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int? CategoriaId { get; set; }
        public string? ImagenUri { get; set; }
        public bool Activo { get; set; }
    }
}
