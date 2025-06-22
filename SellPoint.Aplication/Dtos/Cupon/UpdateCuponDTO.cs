using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Aplication.Dtos.Cupon
{
    public record UpdateCuponDTO
    {
        public int Id { get; init; }
        public string Codigo { get; init; }
        public string Descripcion { get; init; }
        public string TipoDescuento { get; init; } 
        public decimal ValorDescuento { get; init; }
        public decimal MontoMinimo { get; init; }
        public DateTime FechaInicio { get; init; }
        public DateTime FechaVencimiento { get; init; }
        public int? UsosMaximos { get; init; }
        public bool Activo { get; init; }
    }
}
