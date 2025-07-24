using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.View.Models.ModelsCategoria
{
    public abstract record BaseCategoriaDTO
    {
        public string Nombre { get; init; } = string.Empty;
        public string? Descripcion { get; init; }
    }
}
