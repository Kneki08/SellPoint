using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.View.Models.ModelsCategoria
{
    public record RemoveCategoriaDTO : BaseCategoriaDTO
    {
        public int Id { get; init; }
    }
}
