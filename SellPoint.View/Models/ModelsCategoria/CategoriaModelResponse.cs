using SellPoint.View.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.View.Models.ModelsCategoria
{
    public class CategoriaModelResponse : BaseResponse
    {
        public List<CategoriaModel>? data { get; set; }
    }
}
