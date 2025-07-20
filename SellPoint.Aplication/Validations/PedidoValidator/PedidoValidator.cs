using SellPoint.Aplication.Dtos.Pedido;
using SellPoint.Domain.Base;
using SellPoint.Aplication.Validations.Mensajes;
using SellPoint.Domainn.Entities.Orders;

namespace SellPoint.Aplication.Validations.Pedidos
{
    public static class PedidoValidator
    {
        public static OperationResult ValidarSave(SavePedidoDTO dto)
        {
            if (dto == null)
                return OperationResult.Failure(MensajesValidacion.EntidadNula);

            if (dto.IdUsuario <= 0)
                return OperationResult.Failure(MensajesValidacion.UsuarioIdInvalido);

            if (dto.Subtotal < 0)
                return OperationResult.Failure(MensajesValidacion.SubtotalNegativo);

            if (dto.Descuento < 0)
                return OperationResult.Failure(MensajesValidacion.DescuentoNegativo);

            if (dto.CostoEnvio < 0)
                return OperationResult.Failure(MensajesValidacion.CostoEnvioNegativo);

            if (dto.Total != dto.Subtotal - dto.Descuento + dto.CostoEnvio)
                return OperationResult.Failure(MensajesValidacion.TotalInconsistente);

            if (string.IsNullOrWhiteSpace(dto.MetodoPago))
                return OperationResult.Failure(MensajesValidacion.MetodoPagoRequerido);

            if (dto.MetodoPago.Length > 50)
                return OperationResult.Failure(MensajesValidacion.MetodoPagoMuyLargo);

            if (!string.IsNullOrWhiteSpace(dto.ReferenciaPago) && dto.ReferenciaPago.Length > 100)
                return OperationResult.Failure(MensajesValidacion.ReferenciaPagoMuyLarga);

            if (!string.IsNullOrWhiteSpace(dto.Notas) && dto.Notas.Length > 500)
                return OperationResult.Failure(MensajesValidacion.NotasMuyLargas);

            return OperationResult.Success();
        }

        public static OperationResult ValidarUpdate(UpdatePedidoDTO dto)
        {
            if (dto == null)
                return OperationResult.Failure(MensajesValidacion.EntidadNula);

            if (dto.Id <= 0)
                return OperationResult.Failure(MensajesValidacion.PedidoIdInvalido);

            if (string.IsNullOrWhiteSpace(dto.Estado))
                return OperationResult.Failure(MensajesValidacion.EstadoRequerido);

            var estadosValidos = new[] { "EnPreparacion", "Enviado", "Entregado", "Cancelado" };
            if (!estadosValidos.Contains(dto.Estado))
                return OperationResult.Failure(MensajesValidacion.EstadoNoValido);

            if (dto.FechaPedido == DateTime.MinValue || dto.FechaPedido > DateTime.Now.AddMinutes(5))
                return OperationResult.Failure(MensajesValidacion.FechaActualizacionInvalida);

            if (!string.IsNullOrWhiteSpace(dto.MetodoPago) && dto.MetodoPago.Length > 50)
                return OperationResult.Failure(MensajesValidacion.MetodoPagoMuyLargo);

            if (!string.IsNullOrWhiteSpace(dto.ReferenciaPago) && dto.ReferenciaPago.Length > 100)
                return OperationResult.Failure(MensajesValidacion.ReferenciaPagoMuyLarga);

            if (!string.IsNullOrWhiteSpace(dto.Notas) && dto.Notas.Length > 500)
                return OperationResult.Failure(MensajesValidacion.NotasMuyLargas);

            return OperationResult.Success();
        }

        public static OperationResult ValidarRemove(RemovePedidoDTO dto)
        {
            if (dto == null)
                return OperationResult.Failure(MensajesValidacion.EntidadNula);

            if (dto.Id <= 0)
                return OperationResult.Failure(MensajesValidacion.PedidoIdInvalido);

            return OperationResult.Success();
        }

        public static OperationResult ValidarId(int id)
        {
            if (id <= 0)
                return OperationResult.Failure(MensajesValidacion.PedidoIdInvalido);

            return OperationResult.Success();
        }

        public static OperationResult ValidarEntidad(Pedido pedido, bool validarFechaActualizacion = false)
        {
            if (pedido is null)
                return OperationResult.Failure(MensajesValidacion.EntidadNula);

            if (pedido.IdUsuario <= 0)
                return OperationResult.Failure(MensajesValidacion.UsuarioIdInvalido);

            if (pedido.Subtotal < 0)
                return OperationResult.Failure(MensajesValidacion.SubtotalNegativo);

            if (pedido.Descuento < 0)
                return OperationResult.Failure(MensajesValidacion.DescuentoNegativo);

            if (pedido.CostoEnvio < 0)
                return OperationResult.Failure(MensajesValidacion.CostoEnvioNegativo);

            var totalCalculado = pedido.Subtotal - pedido.Descuento + pedido.CostoEnvio;
            if (pedido.Total != totalCalculado)
                return OperationResult.Failure(MensajesValidacion.TotalInconsistente);

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
                    pedido.Fecha_actualizacion > DateTime.Now.AddMinutes(5))
                    return OperationResult.Failure(MensajesValidacion.FechaActualizacionInvalida);
            }

            return OperationResult.Success();
        }
    }
}