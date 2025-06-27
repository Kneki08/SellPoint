using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Aplication.Dtos.Cupon
{
    public record ObtenerCuponDTO
    {
        public int Id { get; init; }
        public string Codigo { get; init; } = string.Empty;
        public decimal Descuento { get; init; }
        public DateTime FechaExpiracion { get; init; }
        public bool Activo { get; init; }
    }
}
