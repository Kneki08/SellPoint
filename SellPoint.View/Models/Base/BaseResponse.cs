using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.View.Models.Base
{
    public class BaseResponse
    {
        public bool isSuccess { get; set; }
        public string? message { get; set; }
    }
}
