using SellPoint.View.Models.ModelsProducto;
using SellPoint.View.Models.ModelsProducto.SellPoint.View.Models.ModelsProducto;

namespace SellPoint.View.Service.ServiceProducto
{
    public interface IProductoService
    {
        Task<bool> AgregarAsync(ProductoModel model);
        Task<bool> ActualizarAsync(ProductoModel model);
        Task<bool> EliminarAsync(int id);
        Task<List<ProductoModel>> ObtenerTodosAsync();
        bool ValidarFormulario(ProductoModel model, out string mensaje);
    }
}


