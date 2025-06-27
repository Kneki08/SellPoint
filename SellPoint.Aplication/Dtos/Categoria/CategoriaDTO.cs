using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Aplication.Dtos.Categoria
{
    public record CategoriaDTO
    {
        public int Id { get; init; }
        public string Nombre { get; init; } = string.Empty;
        public string? Descripcion { get; init; }
        public bool Activo { get; init; }
    }
}
