
using SellPoint.Aplication.Dtos.Carrito;
using SellPoint.Aplication.Dtos.Cupon;
using SellPoint.Aplication.Interfaces.Base;
using SellPoint.Domain.Base;
using SellPoint.Domainn.Entities.Products;

namespace SellPoint.Aplication.Interfaces.Repositorios
{
    public interface ICuponRepository 
    {
        Task<OperationResult> ObtenerPorIdAsync(int id);
        Task<OperationResult> ObtenerTodosAsync();
        Task<OperationResult> AgregarAsync(SaveCuponDTO saveCupon);
        Task<OperationResult> ActualizarAsync(UpdateCuponDTO updateCupon);
        Task<OperationResult> EliminarAsync(RemoveCuponDTIO removeCupon);
    }

}
