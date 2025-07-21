using SellPoint.Aplication.Dtos.Pedido;
using SellPoint.Domain.Base;
using SellPoint.Aplication.Validations.Mensajes;

namespace SellPoint.Aplication.Validations.PedidoValidator
{
    public static class SavePedidoValidator
    {
        public static OperationResult ValidarSave(SavePedidoDTO dto)
        {
            if (dto == null)
                return OperationResult.Failure(MensajesValidacion.EntidadNula);

            if (dto.IdUsuario <= 0)
                return OperationResult.Failure(MensajesValidacion.UsuarioIdInvalido);

            var montosValidos = PedidoMontosValidator.ValidarMontos(dto.Subtotal, dto.Descuento, dto.CostoEnvio, dto.Total);
            if (!montosValidos.IsSuccess)
                return montosValidos;

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
    }
}