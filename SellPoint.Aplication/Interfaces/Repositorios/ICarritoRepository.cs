
using SellPoint.Aplication.Dtos.Carrito;
using SellPoint.Domain.Base;
namespace SellPoint.Aplication.Interfaces.Repositorios
{
    public interface ICarritoRepository
    {
        Task<OperationResult> ObtenerPorIdAsync(int id);
        Task<OperationResult> ObtenerTodosAsync();
        Task<OperationResult> AgregarAsync(SaveCarritoDTO saveCarrito);
        Task<OperationResult> ActualizarAsync(UpdateCarritoDTO updateCarrito);
        Task<OperationResult> EliminarAsync(RemoveCarritoDTO EliminarCarrito);
    }

}
