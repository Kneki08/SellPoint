using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Aplication.Dtos.Categoria
{
    public record CuponDTO
    {
        public int Id { get; init; }
        public string Codigo { get; init; }
        public decimal ValorDescuento { get; init; }
        public DateTime FechaVencimiento { get; init; }
    }
}
