
using SellPoint.Domain.Base;
using SellPoint.Domain.Entities.Orders;
using SellPoint.Aplication.Interfaces.Base;

namespace SellPoint.Aplication.Interfaces.Repositorios
{
    public interface IDetallePedidoRepository : IRepository<DetallePedido>
    {
        Task<OperationResult> ObtenerPorPedidoIdAsync(int pedidoId);
    }

}
