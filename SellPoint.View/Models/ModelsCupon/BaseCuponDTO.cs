using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.View.Models.ModelsCupon
{
    public abstract record BaseCuponDTO
    {
        public string Codigo { get; init; } = string.Empty;
        public string Descripcion { get; init; } = string.Empty;
        public string TipoDescuento { get; init; } = string.Empty; 
        public decimal ValorDescuento { get; init; }
        public decimal MontoMinimo { get; init; }
        public DateTime FechaInicio { get; init; }
        public DateTime FechaVencimiento { get; init; }
        public int? UsosMaximos { get; init; }
        public bool Activo { get; init; }
    }
}
