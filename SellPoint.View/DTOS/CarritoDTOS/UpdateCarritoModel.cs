using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.View.DTOS.CarritoDTOS
{
   public class UpdateCarritoModel : BaseDTOS.BaseCarritoDTOS
    {
        public int Id { get; set; }
        public DateTime? FechaActualizacion { get; set; } = DateTime.Now;
    }
}
