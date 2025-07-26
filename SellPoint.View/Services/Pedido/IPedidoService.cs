using SellPoint.View.Dtos.Pedido;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SellPoint.View.Services.Pedido
{
    public interface IPedidoService
    {
        Task<List<PedidoDTO>> ObtenerTodosAsync();
        Task<(bool exito, string mensaje)> AgregarAsync(SavePedidoDTO dto);
        Task<(bool exito, string mensaje)> ActualizarAsync(UpdatePedidoDTO dto);
        Task<(bool exito, string mensaje)> EliminarAsync(int id);
        Task<PedidoDTO?> ObtenerPorIdAsync(int id);
    }
}