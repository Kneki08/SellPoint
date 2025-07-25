using SellPoint.Aplication.Dtos.DetallePedido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.View.Service.DetallePedidoClient
{
    public interface IDetallePedidoApiClient
    {
        Task<List<DetallePedidoDTO>> ObtenerTodosAsync();
        Task<DetallePedidoDTO> ObtenerPorIdAsync(int id);
        Task<bool> CrearAsync(SaveDetallePedidoDTO dto);
        Task<bool> ActualizarAsync(UpdateDetallePedidoDTO dto);
        Task<bool> EliminarAsync(RemoveDetallePedidoDTO dto);
    }
}
