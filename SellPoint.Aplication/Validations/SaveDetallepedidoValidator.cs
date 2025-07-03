using SellPoint.Aplication.Dtos.DetallePedido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Aplication.Validations.DetallePedidoValidator
{
    public class SaveDetallepedidoValidator : ValidatorBase<SaveDetallePedidoDTO>
    {
        public override bool Validate(SaveDetallePedidoDTO dto, out List<string> errors)
        {
            errors = new List<string>();

            // Validaciones específicas para Save
            if (dto.PrecioUnitario <= 0)
            {
                errors.Add("El precio unitario debe ser mayor que 0");
            }

            // Otras validaciones comunes
            if (dto.ProductoId <= 0) errors.Add("El ID del producto debe ser mayor que 0");
            if (dto.Cantidad <= 0) errors.Add("La cantidad debe ser mayor que 0");
            if (dto.PedidoId <= 0) errors.Add("El ID del pedido debe ser mayor que 0");

            return errors.Count == 0;

        }
    }
}
