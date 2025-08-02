using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.View.Models.ModelsCupon
{
    public class CuponModel
    {
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public decimal ValorDescuento { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string Descripcion { get; internal set; }
        public string TipoDescuento { get; internal set; }
        public decimal? MontoMinimo { get; internal set; }
        public DateTime FechaInicio { get; internal set; }
        public int? UsosMaximos { get; internal set; }
        public int? UsosActuales { get; internal set; }
        public bool Activo { get; internal set; }
        public DateTime FechaCreacion { get; internal set; }
        public DateTime? FechaActualizacion { get; internal set; }
    }
}
