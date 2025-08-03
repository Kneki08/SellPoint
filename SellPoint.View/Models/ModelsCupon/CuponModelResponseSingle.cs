using SellPoint.View.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.View.Models.ModelsCupon
{
    public class CuponModelResponseSingle : BaseResponse
    {
        public CuponModel? data { get; set; }
    }
}
