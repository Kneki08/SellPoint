using SellPoint.Aplication.Dtos.Carrito;
using SellPoint.Aplication.Dtos.Producto;
using SellPoint.Aplication.Interfaces.Base;
using SellPoint.Domain.Base;
using SellPoint.Domainn.Entities.Products;

namespace SellPoint.Aplication.Interfaces.Repositorios
{
    public interface IProductoRepository 
    {

        Task<OperationResult> ObtenerPorIdAsync(int id);
        Task<OperationResult> ObtenerTodosAsync();
        Task<OperationResult> AgregarAsync(SaveProductoDTO entidad);
        Task<OperationResult> ActualizarAsync(UpdateProductoDTO entidad);
        Task<OperationResult> EliminarAsync(RemoveProductoDTO entidad);
    }
        
        

}
