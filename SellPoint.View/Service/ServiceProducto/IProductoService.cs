using SellPoint.View.Models.ModelsProducto;

public interface IProductoService
{
    Task<bool> AgregarAsync(ProductoModel model);
    Task<bool> ActualizarAsync(ProductoModel model);
    Task<bool> EliminarAsync(int id);
    Task<List<ProductoModel>> ObtenerTodosAsync();
    bool ValidarFormulario(ProductoModel model, out string mensaje);
}

