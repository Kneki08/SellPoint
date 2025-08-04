using System.Collections.Generic;
using System.Threading.Tasks;

namespace SellPoint.View.Services.Pedido
{
    public interface IPedidoOpcionesService
    {
        Task<List<int>> ObtenerUsuariosAsync();
        Task<List<int>> ObtenerDireccionesAsync();
        Task<List<string>> ObtenerMetodosPagoAsync();
        Task<List<string>> ObtenerEstadosAsync();
    }
}