using SellPoint.Domain.Base;
using SellPoint.Domain.Entities.Orders;
namespace SellPoint.Aplication.Interfaces.Repositorios
{
    public interface IDetallePedidoRepository : IGenericRepository<DetallePedido>
    {
        Task<OperationResult<List<DetallePedido>>> GetDetallesByPedidoIdAsync(int pedidoId);
    }
}
