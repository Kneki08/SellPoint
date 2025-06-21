
using SellPoint.Domain.Base;
using SellPoint.Domain.Entities.Orders;
using SellPoint.Aplication.Interfaces.Base;
namespace SellPoint.Aplication.Interfaces.Repositorios
{
    public interface ICarritoRepository : IRepository<Carrito>
    {
        Task<OperationResult> ObtenerPorUsuarioIdAsync(int usuarioId);
        Task<OperationResult> AgregarProductoAsync(int carritoId, int productoId, int cantidad);
        Task<OperationResult> EliminarProductoAsync(int carritoId, int productoId);
        Task<OperationResult> VaciarCarritoAsync(int carritoId);
    }

}
