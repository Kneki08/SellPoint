using SellPoint.Domain.Base;
using SellPoint.Domainn.Entities.Orders;
using SellPoint.Aplication.Validations.Mensajes;

namespace SellPoint.Aplication.Validations.PedidoValidator
{
    public static class PedidoEntityValidator
    {
        public static OperationResult ValidarEntidad(Pedido pedido, bool validarFechaActualizacion = false)
        {
            if (pedido is null)
                return OperationResult.Failure(MensajesValidacion.EntidadNula);

            if (pedido.IdUsuario <= 0)
                return OperationResult.Failure(MensajesValidacion.UsuarioIdInvalido);

            var montosValidos = PedidoMontosValidator.ValidarMontos(pedido.Subtotal, pedido.Descuento, pedido.CostoEnvio, pedido.Total);
            if (!montosValidos.IsSuccess)
                return montosValidos;

            if (string.IsNullOrWhiteSpace(pedido.MetodoPago.ToString()))
                return OperationResult.Failure(MensajesValidacion.MetodoPagoRequerido);

            if (pedido.MetodoPago.ToString().Length > 50)
                return OperationResult.Failure(MensajesValidacion.MetodoPagoMuyLargo);

            if (!string.IsNullOrWhiteSpace(pedido.ReferenciaPago) && pedido.ReferenciaPago.Length > 100)
                return OperationResult.Failure(MensajesValidacion.ReferenciaPagoMuyLarga);

            if (!string.IsNullOrWhiteSpace(pedido.Notas) && pedido.Notas.Length > 500)
                return OperationResult.Failure(MensajesValidacion.NotasMuyLargas);

            if (validarFechaActualizacion)
            {
                if (pedido.Fecha_actualizacion == null ||
                    pedido.Fecha_actualizacion == DateTime.MinValue ||
                    pedido.Fecha_actualizacion > DateTime.UtcNow.AddMinutes(5))
                    return OperationResult.Failure(MensajesValidacion.FechaActualizacionInvalida);
            }

            return OperationResult.Success();
        }
    }
}