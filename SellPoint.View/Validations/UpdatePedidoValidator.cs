using SellPoint.View.Models.Pedido;

namespace SellPoint.View.Validations
{
    public static class UpdatePedidoValidator
    {
        public static (bool IsSuccess, string Message) Validar(UpdatePedidoDTO dto)
        {
            if (dto.Id <= 0)
                return (false, MensajesValidacion.IdInvalido);

            var saveValidation = SavePedidoValidator.Validar(dto);
            if (!saveValidation.IsSuccess)
                return saveValidation;

            if (dto.FechaActualizacion < dto.FechaPedido)
                return (false, MensajesValidacion.FechaActualizacionInvalida);

            return (true, "");
        }
    }
}