using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Aplication.Dtos.Categoria
{
    public record SaveCategoriaDTO
    {
        public string Nombre { get; init; } = string.Empty;
        public string? Descripcion { get; init; }
    }
}
