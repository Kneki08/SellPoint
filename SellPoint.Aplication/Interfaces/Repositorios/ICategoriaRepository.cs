using SellPoint.Aplication.Dtos.Carrito;
using SellPoint.Aplication.Interfaces.Base;
using SellPoint.Domain.Base;
using SellPoint.Domainn.Entities.Products;

namespace SellPoint.Aplication.Interfaces.Repositorios
{
    public interface ICategoriaRepository 
    {
        Task<OperationResult> ObtenerPorIdAsync(int id);
        Task<OperationResult> ObtenerTodosAsync();
        Task<OperationResult> AgregarAsync(SaveCarritoDTO entidad);
        Task<OperationResult> ActualizarAsync(UpdateCarritoDTO entidad);
        Task<OperationResult> EliminarAsync(RemoveCarritoDTO entidad);
    }

}
