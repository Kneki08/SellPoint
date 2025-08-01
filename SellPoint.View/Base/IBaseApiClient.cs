using SellPoint.View.Models.ModelDetallePedido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.View.Base
{
    public interface IBaseApiClient<TDto, TSaveDto, TUpdateDto, TRemoveDto>
    {
        Task<ApiResponse<List<TDto>>> ObtenerTodosAsync();
        Task<ApiResponse<TDto>> ObtenerPorIdAsync(int id);
        Task<ApiResponse<bool>> CrearAsync(TSaveDto dto);
        Task<ApiResponse<bool>> ActualizarAsync(TUpdateDto dto);
        Task<ApiResponse<bool>> EliminarAsync(TRemoveDto dto);
    }
}
