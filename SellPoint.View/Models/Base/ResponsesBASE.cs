using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.View.Models.Base
{
   public abstract class ResponsesBASE
    {
       public bool IsSuccess { get; set; }
       public string? Message { get; set; }
    }
}
