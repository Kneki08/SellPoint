using SellPoint.Aplication.Dtos.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Aplication.Validations.ClienteValidator
{
    public class RemoveClienteValidator: ValidatorBase<RemoveClienteDTO>
    {
        public override bool Validate(RemoveClienteDTO dto, out List<string> errors)
        {
            errors = new List<string>();

            if (dto.Id <= 0)
                errors.Add("El ID del cliente debe ser mayor que 0");

            return errors.Count == 0;
        }
    }
}
