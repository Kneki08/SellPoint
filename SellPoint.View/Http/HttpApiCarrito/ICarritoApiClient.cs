using SellPoint.View.DTOS.CarritoDTOS;
using SellPoint.View.Models.ModelsCarrito;
using SellPoint.View.Models.ModelsCarito;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SellPoint.View.Service.ServiceApiCarrito
{
    public interface ICarritoApiClient
    {
        Task<List<CarritoModel>> ObtenerTodosAsync();
        Task<bool> CrearAsync(SaveCarritoModel dto);
        Task<bool> ActualizarAsync(UpdateCarritoModel dto);
        Task<bool> EliminarAsync(RemoveCarritoModel dto);
    }
}
