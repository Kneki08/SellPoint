using SellPoint.Aplication.Dtos.ProductoDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Aplication.Validations.ProductoValidations
{
    public class RemoveProductoValidations : ValidatorBase<RemoveProductoDTO>
    {
        public override bool Validate(RemoveProductoDTO dto, out List<string> errors)
        {
            errors = new List<string>();
            if (dto is null)
            {
                errors.Add("La entidad no puede ser nula.");
                return false;
            }
            if (dto.Id <= 0) 
                errors.Add("El Id del producto debe ser mayor que cero.");
            return errors.Count == 0;
        }
    }
}
