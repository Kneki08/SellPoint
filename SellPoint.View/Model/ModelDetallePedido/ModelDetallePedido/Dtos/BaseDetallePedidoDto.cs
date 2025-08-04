using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.View.Models.ModelDetallePedido.Dtos
{
    public class BaseDetallePedidoDto
    {
 
        public int PedidoId { get; set; }

       
        public int ProductoId { get; set; }

      
        public int Cantidad { get; set; }

      
        public decimal PrecioUnitario { get; set; }

    }
}
