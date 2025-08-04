using SellPoint.View.Models.ModelDetallePedido;
using SellPoint.View.Models.ModelDetallePedido.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.View.Service
{
    public interface IDetallePedidoService
    {
        Task<ApiResponse<IEnumerable<DetalleDto>>> GetAllAsync();
        Task<ApiResponse<DetalleDto>> GetByIdAsync(int id);
        Task<ApiResponse<bool>> CreateAsync(SaveDto dto);
        Task<ApiResponse<bool>> UpdateAsync(UpdateDto dto);
        Task<ApiResponse<bool>> DeleteAsync(int id);
    
    }
}

