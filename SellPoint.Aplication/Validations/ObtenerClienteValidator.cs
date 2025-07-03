using SellPoint.Aplication.Dtos.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SellPoint.Aplication.Validations.ClienteValidator
{
    public class ObtenerClienteValidator : ValidatorBase<ObtenerClienteDTO>
    {
        public override bool Validate(ObtenerClienteDTO dto, out List<string> errors)
        {
            errors = new List<string>();

            if (dto.Id <= 0)
                errors.Add("El ID del cliente debe ser mayor que 0");

            if (string.IsNullOrWhiteSpace(dto.Nombre))
                errors.Add("El nombre del cliente es requerido");

            if (string.IsNullOrWhiteSpace(dto.Apellido))
                errors.Add("El apellido del cliente es requerido");

            if (!string.IsNullOrEmpty(dto.Correo) && !Regex.IsMatch(dto.Correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                errors.Add("El formato del correo electrónico no es válido");

            return errors.Count == 0;
        }

    }
}
