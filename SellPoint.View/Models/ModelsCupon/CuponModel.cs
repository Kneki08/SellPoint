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
    }
}
