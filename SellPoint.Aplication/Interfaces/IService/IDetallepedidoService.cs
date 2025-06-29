
using SellPoint.Aplication.Dtos.DetallePedido;
using SellPoint.Domain.Base;

namespace SellPoint.Aplication.Interfaces.Servicios
{
    public interface IDetallepedidoService
    {
        Task<OperationResult> ObtenerTodosAsync();
        Task<OperationResult> ObtenerPorIdAsync(int ObtenerDetallePedidoDTO);
        Task<OperationResult> AgregarAsync(SaveDetallePedidoDTO saveDetallePedido);
        Task<OperationResult> ActualizarAsync(UpdateDetallePedidoDTO updateDetallePedido);
        Task<OperationResult> EliminarAsync(RemoveDetallePedidoDTO RemoveDetallePedido);


    }
}
