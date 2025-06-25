
using SellPoint.Aplication.Dtos.Carrito;
using SellPoint.Aplication.Dtos.DetallePedido;
using SellPoint.Aplication.Interfaces.Base;
using SellPoint.Domain.Base;
using SellPoint.Domain.Entities.Orders;

namespace SellPoint.Aplication.Interfaces.Repositorios
{
    public interface IDetallePedidoRepository
    {
        Task<OperationResult> ObtenerPorIdAsync(int id);
        Task<OperationResult> ObtenerTodosAsync();
        Task<OperationResult> AgregarAsync(SaveDetallePedidoDTO saveDetallePedido);
        Task<OperationResult> ActualizarAsync(UpdateDetallePedidoDTO updateDetallePedido);
        Task<OperationResult> EliminarAsync(RemoveDetallePedidoDTO removeDetallePedido);
    }

}
