using SellPoint.Aplication.Dtos.DetallePedido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Aplication.Validations.DetallePedidoValidator
{
    public class DetallePedidoValidator : ValidatorBase<DetallePedidoDTO>
    {
        public override bool Validate(DetallePedidoDTO dto, out List<string> errors)
        {
            errors = new List<string>();

            if (dto.ProductoId <= 0)
            {
                errors.Add("El ID del producto debe ser mayor que 0");
            }

            if (dto.Cantidad <= 0)
            {
                errors.Add("La cantidad debe ser mayor que 0");
            }

            if (dto.PedidoId <= 0)
            {
                errors.Add("El ID del pedido debe ser mayor que 0");
            }

            return errors.Count == 0;

        }

    }
}
    
    
