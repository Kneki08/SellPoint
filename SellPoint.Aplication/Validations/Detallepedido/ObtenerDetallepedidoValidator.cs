using SellPoint.Aplication.Dtos.DetallePedido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Aplication.Validations.DetallePedidoValidator
{
    public class ObtenerDetallepedidoValidator : ValidatorBase<ObtenerDetallePedidoDTO>
    {
        public override bool Validate(ObtenerDetallePedidoDTO dto, out List<string> errors)
        {
            errors = new List<string>();

            if (dto.ProductoId <= 0)
                errors.Add("El ID del producto debe ser mayor que 0");

            if (string.IsNullOrWhiteSpace(dto.NombreProducto))
                errors.Add("El nombre del producto es requerido");

            if (dto.Cantidad <= 0)
                errors.Add("La cantidad debe ser mayor que 0");

            if (dto.PrecioUnitario <= 0)
                errors.Add("El precio unitario debe ser mayor que 0");

            if (dto.Subtotal <= 0)
                errors.Add("El subtotal debe ser mayor que 0");

            return errors.Count == 0;
        }

    }
}
