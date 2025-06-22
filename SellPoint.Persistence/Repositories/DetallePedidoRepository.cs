using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;
using SellPoint.Domain.Entities.Orders;

namespace SellPoint.Persistence.Repositories
{
    public class DetallePedidoRepository : IDetallePedidoRepository
    {
        public Task<OperationResult> ActualizarAsync(DetallePedido entidad)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> AgregarAsync(DetallePedido entidad)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> EliminarAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> ObtenerPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> ObtenerPorPedidoIdAsync(int pedidoId)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> ObtenerTodosAsync()
        {
            throw new NotImplementedException();
        }
    }
}
