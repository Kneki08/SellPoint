using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.View.Models.ModelsCategoria
{
    public class CategoriaModelResponseSingle
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public CategoriaModel data { get; set; }
    }
}
