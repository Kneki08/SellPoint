using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.View.Models.Base
{
    public abstract class BaseCuponUpdateModel : BaseCuponModel
    {
        public int Id { get; set; }
        public int? UsosActuales { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
