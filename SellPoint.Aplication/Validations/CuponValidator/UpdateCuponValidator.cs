using SellPoint.Aplication.Dtos.Cupon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Aplication.Validations.CuponValidator
{
    public class UpdateCuponValidator : ValidatorBase<UpdateCuponDTO>
    {
        public override bool Validate(UpdateCuponDTO dto, out List<string> errors)
        {
            errors = new List<string>();

            if (dto is null)
            {
                errors.Add("La entidad no puede ser nula.");
                return false;
            }

            if (dto.Id <= 0)
                errors.Add("El Id del cupón debe ser mayor que cero.");

            if (string.IsNullOrWhiteSpace(dto.Codigo))
                errors.Add("El código del cupón es obligatorio.");

            return errors.Count == 0;
        }
    }
}
