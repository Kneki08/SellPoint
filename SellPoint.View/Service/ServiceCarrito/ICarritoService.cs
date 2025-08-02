using SellPoint.View.Models.ModelsCarito;

namespace SellPoint.View.Service.ServiceCarrito
{
    public interface ICarritoService
    {
        Task<bool> AgregarAsync(CarritoModel model);
        Task<bool> ActualizarAsync(CarritoModel model);
        Task<bool> EliminarAsync(int id);
        Task<List<CarritoModel>> ObtenerTodosAsync();
        bool ValidarFormulario(CarritoModel model, out string mensaje);
    }
}

