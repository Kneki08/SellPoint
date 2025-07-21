using SellPoint.Aplication.Dtos.Pedido;
using SellPoint.Domain.Base;
using SellPoint.Aplication.Validations.Mensajes;

namespace SellPoint.Aplication.Validations.PedidoValidator
{
    public static class RemovePedidoValidator
    {
        public static OperationResult ValidarRemove(RemovePedidoDTO dto)
        {
            if (dto == null)
                return OperationResult.Failure(MensajesValidacion.EntidadNula);

            if (dto.Id <= 0)
                return OperationResult.Failure(MensajesValidacion.PedidoIdInvalido);

            return OperationResult.Success();
        }
    }
}