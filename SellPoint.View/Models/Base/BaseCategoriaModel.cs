using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.View.Models.Base
{
    public abstract class BaseCategoriaModel
    {
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool Activo { get; set; }

        public bool EstaEliminado { get; set; }
    }
}
