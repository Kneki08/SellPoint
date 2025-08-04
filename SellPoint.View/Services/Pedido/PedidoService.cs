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
        private readonly IPedidoValidator _validator;

        public PedidoService(IPedidoApiClient apiClient, IPedidoValidator validator)
        {
            _apiClient = apiClient;
            _validator = validator;
        }

        public async Task<ApiResponse<List<PedidoDTO>>> ObtenerTodosAsync()
        {
            return await _apiClient.ObtenerTodosAsync();
        }

        public async Task<ApiResponse<PedidoDTO>> ObtenerPorIdAsync(int id)
        {
            var (valido, mensaje) = _validator.ValidarId(id);
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
            var (valido, mensaje) = _validator.ValidarGuardar(dto);
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
            var (valido, mensaje) = _validator.ValidarActualizar(dto);
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
            var (valido, mensaje) = _validator.ValidarEliminar(id);
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