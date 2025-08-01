using SellPoint.Aplication.Dtos.DetallePedido;
using SellPoint.View.Models.ModelDetallePedido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.View.Repositories
{
    public interface IDetallePedidoRepository
    {
        Task<ApiResponse<IEnumerable<DetallePedidoDTO>>> GetAllAsync();
        Task<ApiResponse<DetallePedidoDTO>> GetByIdAsync(int id);
        Task<ApiResponse<bool>> CreateAsync(SaveDetallePedidoDTO dto);
        Task<ApiResponse<bool>> UpdateAsync(UpdateDetallePedidoDTO dto);
        Task<ApiResponse<bool>> DeleteAsync(int id);
    }
}
