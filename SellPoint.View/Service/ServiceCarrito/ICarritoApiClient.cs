using SellPoint.Aplication.Dtos.Carrito;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SellPoint.View.Service.ServiceCarrito
{
    public interface ICarritoApiClient
    {
        Task<List<ObtenerCarritoDTO>> ObtenerTodosAsync();
        Task<bool> CrearAsync(SaveCarritoDTO dto);
        Task<bool> ActualizarAsync(UpdateCarritoDTO dto);
        Task<bool> EliminarAsync(RemoveCarritoDTO dto);
    }
}
