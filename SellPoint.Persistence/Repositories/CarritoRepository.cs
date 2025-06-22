

using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;
using SellPoint.Domain.Entities.Orders;

namespace SellPoint.Persistence.Repositories
{
    public class CarritoRepository : ICarritoRepository
    {
        public Task<OperationResult> ActualizarAsync(Carrito entidad)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> AgregarAsync(Carrito entidad)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> AgregarProductoAsync(int carritoId, int productoId, int cantidad)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> EliminarAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> EliminarProductoAsync(int carritoId, int productoId)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> ObtenerPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> ObtenerPorUsuarioIdAsync(int usuarioId)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> ObtenerTodosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> VaciarCarritoAsync(int carritoId)
        {
            throw new NotImplementedException();
        }
    }
}
