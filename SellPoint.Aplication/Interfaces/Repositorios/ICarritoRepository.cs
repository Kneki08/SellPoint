
using SellPoint.Aplication.Dtos.Carrito;
using SellPoint.Domain.Base;
namespace SellPoint.Aplication.Interfaces.Repositorios
{
    public interface ICarritoRepository
    {
        Task<OperationResult> ObtenerPorIdAsync(int id);
        Task<OperationResult> ObtenerTodosAsync();
        Task<OperationResult> AgregarAsync(SaveCarritoDTO entidad);
        Task<OperationResult> ActualizarAsync(UpdateCarritoDTO entidad);
        Task<OperationResult> EliminarAsync(RemoveCarritoDTO entidad);
    }

}
