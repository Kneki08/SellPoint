using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.View.Models.ModelDetallePedido.Dtos
{
    public class DetallePedidoDto
    {
        public int Id { get; set; }

        public int PedidoId { get; set; }

        public int ProductoId { get; set; }

        public int Cantidad { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal PrecioUnitario { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Subtotal => Cantidad * PrecioUnitario;

        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
