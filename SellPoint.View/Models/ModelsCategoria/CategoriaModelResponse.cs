using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.View.Models.ModelsCategoria
{
    public class CategoriaModelResponse
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public List<CategoriaModel> data { get; set; }
    }
}
