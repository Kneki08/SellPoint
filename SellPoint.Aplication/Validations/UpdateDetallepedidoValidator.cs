using SellPoint.Aplication.Dtos.DetallePedido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Aplication.Validations.DetallePedidoValidator
{
    public class UpdateDetallepedidoValidator : ValidatorBase<UpdateDetallePedidoDTO>
    {
        public override bool Validate(UpdateDetallePedidoDTO dto, out List<string> errors)
        {
            errors = new List<string>();

            if (dto.Id <= 0)
                errors.Add("El ID del detalle debe ser mayor que 0");

            if (dto.Cantidad <= 0)
                errors.Add("La cantidad debe ser mayor que 0");

            if (dto.PrecioUnitario <= 0)
                errors.Add("El precio unitario debe ser mayor que 0");

            if (dto.Subtotal <= 0)
                errors.Add("El subtotal debe ser mayor que 0");

            if (dto.PedidoId <= 0)
                errors.Add("El ID del pedido debe ser mayor que 0");

            if (dto.ProductoId <= 0)
                errors.Add("El ID del producto debe ser mayor que 0");

            return errors.Count == 0;
        }
    }
}

