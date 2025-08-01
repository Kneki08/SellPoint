using SellPoint.View.Base;
using SellPoint.View.Models.ModelDetallePedido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.View.Service.DetallePedidoClient.Contract
{
    public interface IDetallePedidoApiClient :
         IBaseApiClient<DetallePedidoDTO, SaveDetallePedidoDTO, UpdateDetallePedidoDTO, RemoveDetallePedidoDTO>
    {
        // Puedes gregar métodos específicos adicionales aquí si son necesarios
        Task<ApiResponse<List<DetallePedidoDTO>>> ObtenerPorIdPedidoAsync(int idPedido);

    }
}
