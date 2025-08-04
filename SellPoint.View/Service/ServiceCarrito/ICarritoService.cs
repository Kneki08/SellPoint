using SellPoint.View.Models.ModelsCarito;
using SellPoint.View.Models.ModelsCarrito;

namespace SellPoint.View.Service.ServiceCarrito
{
    public interface ICarritoService
    {
        Task<bool> AgregarAsync(CarritoModel model);
        Task<bool> ActualizarAsync(CarritoModel model);
        Task<bool> EliminarAsync(int id);
        Task<List<CarritoModel>> ObtenerTodosAsync();
        bool Validar(CarritoModel model, out string mensaje);
    }
}
   


