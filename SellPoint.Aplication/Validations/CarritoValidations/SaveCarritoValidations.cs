using SellPoint.Aplication.Dtos.Carrito;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Aplication.Validations.CarritoValidations
{
    public class SaveCarritoValidations : ValidatorBase<SaveCarritoDTO>
    {
        public override bool Validate(SaveCarritoDTO dto, out List<string> errors)
        {
            errors = new List<string>();
            if (dto is null)
            {
                errors.Add("La entidad no puede ser nula.");
                return false;
            }
            if (dto.ProductoId <= 0)
                errors.Add("El Id del producto debe ser mayor que cero.");
            if (dto.UsuarioId <= 0)
                errors.Add("El Id del usuario debe ser mayor que cero.");
            if (dto.Cantidad <= 0)
                errors.Add("La cantidad debe ser mayor que cero.");


            return errors.Count == 0;
        }
    }
}
