using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.View.Models.Base
{
    public abstract class BaseCategoriaUpdateModel : BaseCategoriaModel
    {
        public int Id { get; set; }
        public bool EstaEliminado { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}
