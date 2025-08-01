using SellPoint.Aplication.Dtos.DetallePedido;
using SellPoint.View.Models.ModelDetallePedido;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SellPoint.View.Models.ModelDetallePedido.Request;

namespace SellPoint.View.Validations
{
    public static class DetallePedidoValidator
    {
        public static void Validate(SaveDetallePedidoDTO dto)
        {
            var request = new DetallePedidoRequest
            {
                PedidoId = dto.PedidoId,
                ProductoId = dto.ProductoId,
                Cantidad = dto.Cantidad,
                PrecioUnitario = dto.PrecioUnitario
            };

            Validator.ValidateObject(request, new ValidationContext(request), true);
        }

        public static void Validate(UpdateDetallePedidoDTO dto)
        {
            var request = new DetallePedidoRequest
            {
                PedidoId = dto.PedidoId,
                ProductoId = dto.ProductoId,
                Cantidad = dto.Cantidad,
                PrecioUnitario = dto.PrecioUnitario
            };

            Validator.ValidateObject(request, new ValidationContext(request), true);
        }
    }
}
