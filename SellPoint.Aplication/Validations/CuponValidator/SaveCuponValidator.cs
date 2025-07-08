using SellPoint.Aplication.Dtos.Cupon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Aplication.Validations.CuponValidator
{
    public class SaveCuponValidator : ValidatorBase<SaveCuponDTO>
    {
        public override bool Validate(SaveCuponDTO dto, out List<string> errors)
        {
            errors = new List<string>();

            if (dto is null)
            {
                errors.Add("La entidad no puede ser nula.");
                return false;
            }

            Validate(dto.Codigo, errors, "El código del cupón es obligatorio.");
            return errors.Count == 0;
        }

        private void Validate(string? value, List<string> errors, string message)
        {
            if (string.IsNullOrWhiteSpace(value))
                errors.Add(message);
        }
    }
}
