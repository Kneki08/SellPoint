using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.View.Models.ModelsCupon
{
    public class CuponModelResponseSingle
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public CuponModel data { get; set; }
    }
}
