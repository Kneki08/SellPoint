using SellPoint.Aplication.Dtos.Categoria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Aplication.Validations.CategoriaValidator
{
    public class SaveCategoriaValidator : ValidatorBase<SaveCategoriaDTO>
    {
        public override bool Validate(SaveCategoriaDTO dto, out List<string> errors)
        {
            errors = new List<string>();

            if (dto is null)
            {
                errors.Add("La entidad no puede ser nula.");
                return false;
            }

            Validate(dto.Nombre, errors, "El nombre de la categoría es obligatorio.");
            return errors.Count == 0;
        }

        private void Validate(string? value, List<string> errors, string message)
        {
            if (string.IsNullOrWhiteSpace(value))
                errors.Add(message);
        }
    }

}
