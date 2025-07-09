using SellPoint.Aplication.Dtos.Carrito;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Aplication.Validations.CarritoValidations
{
    public class RemoveCarritoValidations : ValidatorBase<RemoveCarritoDTO>
    {
        public override bool Validate(RemoveCarritoDTO dto, out List<string> errors)
        {
           errors = new List<string>();
            if (dto is null)
            {
                errors.Add("La entidad no puede ser nula.");
                return false;
            }
            if (dto.ProductoId <= 0)
                errors.Add("El Id del carrito debe ser mayor que cero.");
            if (dto.UsuarioId <= 0)
                errors.Add("El Id del usuario debe ser mayor que cero.");
            return errors.Count == 0;
        }
    }
}
