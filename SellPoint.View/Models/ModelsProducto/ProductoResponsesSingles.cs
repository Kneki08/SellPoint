using SellPoint.View.Models.Base;
using SellPoint.View.Models.ModelsProducto.SellPoint.View.Models.ModelsProducto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.View.Models.ModelsProducto
{
    public class ProductoResponsesSingles : ResponsesBASE
    {
        public ProductoModel? data { get; set; }
    }
}
