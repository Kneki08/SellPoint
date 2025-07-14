using SellPoint.Aplication.Dtos.Producto;
using SellPoint.Aplication.Dtos.ProductoDTO;
using SellPoint.Domain.Base;

namespace SellPoint.Aplication.Interfaces.IService.IProducto
{
    public interface IProductoService
    {
        Task<OperationResult> ObtenerPorIdAsync(int id);
        Task<OperationResult> ObtenerTodosAsync();
        Task<OperationResult> AgregarAsync(SaveProductoDTO entidad);
        Task<OperationResult> ActualizarAsync(UpdateProductoDTO entidad);
        Task<OperationResult> EliminarAsync(RemoveProductoDTO entidad);
    }
}
