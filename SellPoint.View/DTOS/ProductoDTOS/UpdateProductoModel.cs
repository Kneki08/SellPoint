using SellPoint.View.DTOS.BaseDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.View.DTOS.ProductoDTOS
{
    public class UpdateProductoModel : BaseProductoDTOS
    {
        public int Id { get; set; }
        public bool Activo { get; set; } = true;
    }
}
