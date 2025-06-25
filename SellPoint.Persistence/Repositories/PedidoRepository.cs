using SellPoint.Aplication.Dtos.Pedido;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;
using SellPoint.Domainn.Entities.Orders;

namespace SellPoint.Persistence.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        public Task<OperationResult> ActualizarAsync(UpdatePedidoDTO updatePedido)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> AgregarAsync(SavePedidoDTO savePedido)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> EliminarAsync(RemovePedidoDTO removePedido)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> ObtenerPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> ObtenerTodosAsync()
        {
            throw new NotImplementedException();
        }
    }
}

