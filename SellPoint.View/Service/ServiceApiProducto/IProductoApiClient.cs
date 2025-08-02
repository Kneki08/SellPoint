using SellPoint.Aplication.Dtos.Producto;
using SellPoint.Aplication.Dtos.ProductoDTO;

namespace SellPoint.View.Service.ServiceApiProducto
{
    public interface IProductoApiClient
    {
        Task<List<ProductoDTO>> ObtenerTodosAsync();
        Task<bool> CrearAsync(SaveProductoDTO dto);
        Task<bool> ActualizarAsync(UpdateProductoDTO dto);
        Task<bool> EliminarAsync(RemoveProductoDTO dto);
    }
}
