using SellPoint.Aplication.Dtos.Pedido;
using SellPoint.Domain.Base;

namespace SellPoint.Aplication.Interfaces.IService
{
    public interface IPedidoService
    {
        Task<OperationResult> ObtenerPorIdAsync(int id);
        Task<OperationResult> ObtenerTodosAsync();
        Task<OperationResult> AgregarAsync(SavePedidoDTO entidad);
        Task<OperationResult> ActualizarAsync(UpdatePedidoDTO entidad);
        Task<OperationResult> EliminarAsync(RemovePedidoDTO entidad);
    }
}