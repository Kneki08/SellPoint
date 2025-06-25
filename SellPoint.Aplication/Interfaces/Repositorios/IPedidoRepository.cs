
using SellPoint.Aplication.Dtos.Carrito;
using SellPoint.Aplication.Dtos.Pedido;
using SellPoint.Aplication.Interfaces.Base;
using SellPoint.Domain.Base;
using SellPoint.Domainn.Entities.Orders;

namespace SellPoint.Aplication.Interfaces.Repositorios
{
    public interface IPedidoRepository 
    {
        Task<OperationResult> ObtenerPorIdAsync(int id);
        Task<OperationResult> ObtenerTodosAsync();
        Task<OperationResult> AgregarAsync(SavePedidoDTO savePedido);
        Task<OperationResult> ActualizarAsync(UpdatePedidoDTO updatePedido);
        Task<OperationResult> EliminarAsync(RemovePedidoDTO removePedido);
    }

}
