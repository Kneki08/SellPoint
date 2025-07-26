using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.View.Models.ModelsCarito
{
    public class CarritoModel
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Total => Cantidad * PrecioUnitario;
        public bool Activo { get; set; }
        public object ClienteId { get; internal set; }
        public DateTime FechaAgregado { get; internal set; }
        public object Estado { get; internal set; }
    }
}
