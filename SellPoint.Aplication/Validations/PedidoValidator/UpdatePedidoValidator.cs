using SellPoint.Aplication.Dtos.Pedido;
using SellPoint.Domain.Base;
using SellPoint.Aplication.Validations.Mensajes;

namespace SellPoint.Aplication.Validations.PedidoValidator
{
    public static class UpdatePedidoValidator
    {
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

            var montosValidos = PedidoMontosValidator.ValidarMontos(dto.Subtotal, dto.Descuento, dto.CostoEnvio, dto.Total);
            if (!montosValidos.IsSuccess)
                return montosValidos;

            if (!string.IsNullOrWhiteSpace(dto.MetodoPago) && dto.MetodoPago.Length > 50)
                return OperationResult.Failure(MensajesValidacion.MetodoPagoMuyLargo);

            if (!string.IsNullOrWhiteSpace(dto.ReferenciaPago) && dto.ReferenciaPago.Length > 100)
                return OperationResult.Failure(MensajesValidacion.ReferenciaPagoMuyLarga);

            if (!string.IsNullOrWhiteSpace(dto.Notas) && dto.Notas.Length > 500)
                return OperationResult.Failure(MensajesValidacion.NotasMuyLargas);

            if (dto.FechaActualizacion > DateTime.UtcNow)
                return OperationResult.Failure(MensajesValidacion.FechaActualizacionInvalida);

            return OperationResult.Success();
        }
    }
}