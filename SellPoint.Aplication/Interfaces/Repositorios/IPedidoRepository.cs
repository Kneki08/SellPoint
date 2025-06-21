
using SellPoint.Domain.Base;
using SellPoint.Domain.Entities.Orders;
using SellPoint.Domainn.Entities.Orders;

namespace SellPoint.Aplication.Interfaces.Repositorios
{
    public interface IPedidoRepository : IGenericRepository<DetallePedido>
    {
        Task<OperationResult<List<Pedido>>> GetPedidosByClienteIdAsync(int clienteId);
        Task<OperationResult> ChangeStatusPedidoAsync(int pedidoId, string nuevoEstado);
    }
}
