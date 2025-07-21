using SellPoint.Domain.Base;
using SellPoint.Aplication.Validations.Mensajes;

namespace SellPoint.Aplication.Validations.PedidoValidator
{
    public static class PedidoIdValidator
    {
        public static OperationResult ValidarId(int id)
        {
            if (id <= 0)
                return OperationResult.Failure(MensajesValidacion.PedidoIdInvalido);

            return OperationResult.Success();
        }
    }
}