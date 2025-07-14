using SellPoint.Aplication.Dtos.Carrito;
using SellPoint.Domain.Base;

namespace SellPoint.Aplication.Interfaces.IService.ICarrito
{
    public interface ICarritoService
    {
        Task<OperationResult> ObtenerTodosAsync();
        Task<OperationResult> ObtenerPorIdAsync(int ObtenerCarritoDTO);
        Task<OperationResult> AgregarAsync(SaveCarritoDTO saveCarrito);
        Task<OperationResult> ActualizarAsync(UpdateCarritoDTO updateCarrito);
        Task<OperationResult> EliminarAsync(RemoveCarritoDTO EliminarCarrito);


    }
}
