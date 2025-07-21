using SellPoint.Aplication.Dtos.Pedido;
using SellPoint.Domain.Base;
using SellPoint.Domainn.Entities.Orders;

namespace SellPoint.Aplication.Validations.PedidoValidator
{
    public static class PedidoValidator
    {
        public static OperationResult ValidarSave(SavePedidoDTO dto)
        {
            return SavePedidoValidator.ValidarSave(dto);
        }

        public static OperationResult ValidarUpdate(UpdatePedidoDTO dto)
        {
            return UpdatePedidoValidator.ValidarUpdate(dto);
        }

        public static OperationResult ValidarRemove(RemovePedidoDTO dto)
        {
            return RemovePedidoValidator.ValidarRemove(dto);
        }

        public static OperationResult ValidarId(int id)
        {
            return PedidoIdValidator.ValidarId(id);
        }

        public static OperationResult ValidarEntidad(Pedido pedido, bool validarFechaActualizacion = false)
        {
            return PedidoEntityValidator.ValidarEntidad(pedido, validarFechaActualizacion);
        }
    }
}