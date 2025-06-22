using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;
using SellPoint.Domainn.Entities.Orders;

namespace SellPoint.Persistence.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        public Task<OperationResult> ActualizarAsync(Pedido entidad)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> AgregarAsync(Pedido entidad)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> CambiarEstadoAsync(int pedidoId, string nuevoEstado)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> EliminarAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> ObtenerPedidoConDetallesAsync(int pedidoId)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> ObtenerPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> ObtenerPorUsuarioAsync(int usuarioId)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> ObtenerTodosAsync()
        {
            throw new NotImplementedException();
        }
    }
}
