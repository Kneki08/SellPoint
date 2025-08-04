using SellPoint.View.DTOS.ProductoDTOS;
using SellPoint.View.Models.ModelsProducto;
using SellPoint.View.Models.ModelsProducto.SellPoint.View.Models.ModelsProducto;

namespace SellPoint.View.Http.ServiceApiProducto
{
    public interface IProductoApiClient
    {
        Task<List<ProductoModel>> ObtenerTodosAsync();
        Task<bool> CrearAsync(SaveProductoModel model);
        Task<bool> ActualizarAsync(UpdateProductoModel model);
        Task<bool> EliminarAsync(RemoveProductoModel model);
    }
}

