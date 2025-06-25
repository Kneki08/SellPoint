using SellPoint.Aplication.Dtos.DetallePedido;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;


namespace SellPoint.Persistence.Repositories
{
    public class DetallePedidoRepository : IDetallePedidoRepository
    {
        public Task<OperationResult> ActualizarAsync(UpdateDetallePedidoDTO updateDetallePedido)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> AgregarAsync(SaveDetallePedidoDTO saveDetallePedido)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> EliminarAsync(RemoveDetallePedidoDTO removeDetallePedido)
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
