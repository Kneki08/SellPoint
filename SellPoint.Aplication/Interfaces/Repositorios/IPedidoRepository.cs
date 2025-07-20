using SellPoint.Aplication.Dtos.Pedido;
using SellPoint.Domain.Base;
using SellPoint.Domainn.Entities.Orders;

namespace SellPoint.Aplication.Interfaces.Repositorios
{
    public interface IPedidoRepository
    {
        Task<OperationResult> ObtenerPorIdAsync(int id);
        Task<OperationResult> ObtenerTodosAsync();
        Task<OperationResult> AgregarAsync(Pedido pedido);
        Task<OperationResult> ActualizarAsync(Pedido pedido);
        Task<OperationResult> EliminarAsync(RemovePedidoDTO removePedido);
    }
}