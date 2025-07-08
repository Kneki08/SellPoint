using SellPoint.Aplication.Dtos.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SellPoint.Aplication.Validations.ClienteValidator
{
    public class UpdateClienteValidator : ValidatorBase<UpdateClienteDTO>
    {
        public override bool Validate(UpdateClienteDTO dto, out List<string> errors)
        {
            errors = new List<string>();

            if (dto.Id <= 0)
                errors.Add("El ID del cliente debe ser mayor que 0");

            if (string.IsNullOrWhiteSpace(dto.Nombre))
                errors.Add("El nombre del cliente es requerido");
            else if (dto.Nombre.Length > 50)
                errors.Add("El nombre no puede exceder los 50 caracteres");

            if (string.IsNullOrWhiteSpace(dto.Apellido))
                errors.Add("El apellido del cliente es requerido");
            else if (dto.Apellido.Length > 50)
                errors.Add("El apellido no puede exceder los 50 caracteres");

            if (string.IsNullOrWhiteSpace(dto.Email))
                errors.Add("El email es requerido");
            else if (!Regex.IsMatch(dto.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                errors.Add("El formato del email no es válido");

            if (string.IsNullOrWhiteSpace(dto.Telefono))
                errors.Add("El teléfono es requerido");
            else if (!Regex.IsMatch(dto.Telefono, @"^[0-9]{7,15}$"))
                errors.Add("El formato del teléfono no es válido");

            if (dto.FechaNacimiento.HasValue && dto.FechaNacimiento > DateTime.Now)
                errors.Add("La fecha de nacimiento no puede ser futura");

            return errors.Count == 0;
        }
    }
}
