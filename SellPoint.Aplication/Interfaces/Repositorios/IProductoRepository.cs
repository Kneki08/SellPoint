
using SellPoint.Aplication.Dtos.Producto;
using SellPoint.Aplication.Dtos.ProductoDTO;
using SellPoint.Domain.Base;


namespace SellPoint.Aplication.Interfaces.Repositorios
{
    public interface IProductoRepository 
    {
        Task<OperationResult> ObtenerTodosAsync();
        Task<OperationResult> ObtenerPorIdAsync(int obtenerProducto);
        Task<OperationResult> AgregarAsync(SaveProductoDTO saveProducto);
        Task<OperationResult> ActualizarAsync(UpdateProductoDTO updateProducto);
        Task<OperationResult> EliminarAsync(RemoveProductoDTO removeProducto);
    }
        
        

}
