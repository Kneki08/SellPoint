using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.View.Models.Base
{
    public abstract class BaseCuponModel
    {
        public string? Codigo { get; set; }
        public string? Descripcion { get; set; }
        public string? TipoDescuento { get; set; }
        public decimal ValorDescuento { get; set; }
        public decimal? MontoMinimo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int? UsosMaximos { get; set; }
        public bool Activo { get; set; }
    }
}
