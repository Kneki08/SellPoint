using SellPoint.Aplication.Dtos.DetallePedido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Aplication.Validations.DetallePedidoValidator
{
    public class RemoveDetallePedidoValidator: ValidatorBase<RemoveDetallePedidoDTO>
    {
        public override bool Validate(RemoveDetallePedidoDTO dto, out List<string> errors)
        {
            errors = new List<string>();

            if (dto.Id <= 0)
                errors.Add("El ID del detalle debe ser mayor que 0");

            return errors.Count == 0;
        }

    }
}
