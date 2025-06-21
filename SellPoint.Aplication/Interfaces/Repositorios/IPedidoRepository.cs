
using SellPoint.Aplication.Interfaces.Base;
using SellPoint.Domain.Base;
using SellPoint.Domainn.Entities.Orders;

namespace SellPoint.Aplication.Interfaces.Repositorios
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        Task<OperationResult> ObtenerPorUsuarioAsync(int usuarioId);
        Task<OperationResult> ObtenerPedidoConDetallesAsync(int pedidoId);
        Task<OperationResult> CambiarEstadoAsync(int pedidoId, string nuevoEstado);
    }

}
