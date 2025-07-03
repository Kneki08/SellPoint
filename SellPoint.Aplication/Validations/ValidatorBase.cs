using SellPoint.Aplication.Dtos.DetallePedido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Aplication.Validations
{
    public abstract class ValidatorBase<T>
    {
       public abstract bool Validate(T dto, out List<string> errors);
    }
}
