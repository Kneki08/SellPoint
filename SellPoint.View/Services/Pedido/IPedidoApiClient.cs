using SellPoint.View.Models.Pedido;
using SellPoint.View.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SellPoint.View.Services.Pedido
{
    public interface IPedidoApiClient
    {
        Task<ApiResponse<List<PedidoDTO>>> ObtenerTodosAsync();
        Task<ApiResponse<PedidoDTO>> ObtenerPorIdAsync(int id);
        Task<ApiResponse<bool>> AgregarAsync(SavePedidoDTO dto);
        Task<ApiResponse<bool>> ActualizarAsync(UpdatePedidoDTO dto);
        Task<ApiResponse<bool>> EliminarAsync(int id);
    }
}