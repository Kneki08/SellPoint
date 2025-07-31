using SellPoint.View.Models.Pedido;
using SellPoint.View.Models;
using SellPoint.View.Validations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SellPoint.View.Services.Pedido
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoApiClient _apiClient;

        public PedidoService(IPedidoApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<ApiResponse<List<PedidoDTO>>> ObtenerTodosAsync()
        {
            // Sin validaciones adicionales aquí, ya que es una consulta global.
            return await _apiClient.ObtenerTodosAsync();
        }

        public async Task<ApiResponse<PedidoDTO>> ObtenerPorIdAsync(int id)
        {
            var (valido, mensaje) = PedidoIdValidator.Validar(id);
            if (!valido)
            {
                return new ApiResponse<PedidoDTO>
                {
                    IsSuccess = false,
                    Message = mensaje,
                    Data = null
                };
            }
            return await _apiClient.ObtenerPorIdAsync(id);
        }

        public async Task<ApiResponse<bool>> AgregarAsync(SavePedidoDTO dto)
        {
            var (valido, mensaje) = SavePedidoValidator.Validar(dto);
            if (!valido)
            {
                return new ApiResponse<bool>
                {
                    IsSuccess = false,
                    Message = mensaje,
                    Data = false
                };
            }
            return await _apiClient.AgregarAsync(dto);
        }

        public async Task<ApiResponse<bool>> ActualizarAsync(UpdatePedidoDTO dto)
        {
            var (valido, mensaje) = UpdatePedidoValidator.Validar(dto);
            if (!valido)
            {
                return new ApiResponse<bool>
                {
                    IsSuccess = false,
                    Message = mensaje,
                    Data = false
                };
            }
            return await _apiClient.ActualizarAsync(dto);
        }

        public async Task<ApiResponse<bool>> EliminarAsync(int id)
        {
            var (valido, mensaje) = RemovePedidoValidator.Validar(id);
            if (!valido)
            {
                return new ApiResponse<bool>
                {
                    IsSuccess = false,
                    Message = mensaje,
                    Data = false
                };
            }
            return await _apiClient.EliminarAsync(id);
        }
    }
}