using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SellPoint.View.Helpers;
using SellPoint.View.Models;
using SellPoint.View.Models.Pedido;
using SellPoint.View.Services.Pedido.Api.Client;
using SellPoint.View.Validations;

namespace SellPoint.View.Services.Pedido.Pedido.Service
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoApiClient _apiClient;
        private readonly IPedidoValidator _validator;
        private readonly ILogger<PedidoService> _logger;

        public PedidoService(
            IPedidoApiClient apiClient,
            IPedidoValidator validator,
            ILogger<PedidoService> logger)
        {
            _apiClient = apiClient;
            _validator = validator;
            _logger = logger;
        }

        public async Task<ApiResponse<List<PedidoDTO>>> ObtenerTodosAsync()
        {
            _logger.LogInformation(PedidoMensajes.LogIntentoObtenerTodos);
            try
            {
                var response = await _apiClient.ObtenerTodosAsync();
                if (response.IsSuccess)
                    _logger.LogInformation(PedidoMensajes.LogObtenerTodosExito, response.Data?.Count ?? 0);
                else
                    _logger.LogWarning(PedidoMensajes.LogErrorObtenerTodos, response.Message);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, PedidoMensajes.LogErrorObtenerTodos, ex.Message);
                return new ApiResponse<List<PedidoDTO>>
                {
                    IsSuccess = false,
                    Message = "Error al obtener pedidos",
                    Data = null
                };
            }
        }

        public async Task<ApiResponse<PedidoDTO>> ObtenerPorIdAsync(int id)
        {
            _logger.LogInformation(PedidoMensajes.LogIntentoCargar, id);

            var (valido, mensaje) = _validator.ValidarId(id);
            if (!valido)
            {
                _logger.LogWarning(PedidoMensajes.LogNoSePudoCargar, id, mensaje);
                return new ApiResponse<PedidoDTO>
                {
                    IsSuccess = false,
                    Message = mensaje,
                    Data = null
                };
            }

            try
            {
                var response = await _apiClient.ObtenerPorIdAsync(id);
                if (response.IsSuccess)
                    _logger.LogInformation("Pedido {Id} cargado correctamente", id);
                else
                    _logger.LogWarning(PedidoMensajes.LogNoSePudoCargar, id, response.Message);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepción en ObtenerPorIdAsync para ID {Id}", id);
                return new ApiResponse<PedidoDTO>
                {
                    IsSuccess = false,
                    Message = $"Error al cargar pedido {id}",
                    Data = null
                };
            }
        }

        public async Task<ApiResponse<bool>> AgregarAsync(SavePedidoDTO dto)
        {
            _logger.LogInformation(PedidoMensajes.LogIntentoAgregar);

            var (valido, mensaje) = _validator.ValidarGuardar(dto);
            if (!valido)
            {
                _logger.LogWarning(PedidoMensajes.LogValidacionFallidaAgregar, mensaje);
                return new ApiResponse<bool>
                {
                    IsSuccess = false,
                    Message = mensaje,
                    Data = false
                };
            }

            try
            {
                var response = await _apiClient.AgregarAsync(dto);
                if (response.IsSuccess)
                    _logger.LogInformation(PedidoMensajes.LogAgregadoExito);
                else
                    _logger.LogWarning(PedidoMensajes.LogErrorAgregar, response.Message);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, PedidoMensajes.LogErrorAgregar, ex.Message);
                return new ApiResponse<bool>
                {
                    IsSuccess = false,
                    Message = "Error al agregar pedido",
                    Data = false
                };
            }
        }

        public async Task<ApiResponse<bool>> ActualizarAsync(UpdatePedidoDTO dto)
        {
            _logger.LogInformation(PedidoMensajes.LogIntentoActualizar, dto.Id);

            var (valido, mensaje) = _validator.ValidarActualizar(dto);
            if (!valido)
            {
                _logger.LogWarning(PedidoMensajes.LogValidacionFallidaActualizar, mensaje);
                return new ApiResponse<bool>
                {
                    IsSuccess = false,
                    Message = mensaje,
                    Data = false
                };
            }

            try
            {
                var response = await _apiClient.ActualizarAsync(dto);
                if (response.IsSuccess)
                    _logger.LogInformation(PedidoMensajes.LogActualizadoExito);
                else
                    _logger.LogWarning(PedidoMensajes.LogErrorActualizar, dto.Id, response.Message);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, PedidoMensajes.LogErrorActualizar, dto.Id, ex.Message);
                return new ApiResponse<bool>
                {
                    IsSuccess = false,
                    Message = $"Error al actualizar pedido {dto.Id}",
                    Data = false
                };
            }
        }

        public async Task<ApiResponse<bool>> EliminarAsync(int id)
        {
            _logger.LogInformation(PedidoMensajes.LogIntentoEliminar, id);

            var (valido, mensaje) = _validator.ValidarEliminar(id);
            if (!valido)
            {
                _logger.LogWarning(PedidoMensajes.LogErrorEliminar, id, mensaje);
                return new ApiResponse<bool>
                {
                    IsSuccess = false,
                    Message = mensaje,
                    Data = false
                };
            }

            try
            {
                var response = await _apiClient.EliminarAsync(id);
                if (response.IsSuccess)
                    _logger.LogInformation("Pedido eliminado exitosamente, ID {Id}", id);
                else
                    _logger.LogWarning(PedidoMensajes.LogErrorEliminar, id, response.Message);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, PedidoMensajes.LogErrorEliminar, id, ex.Message);
                return new ApiResponse<bool>
                {
                    IsSuccess = false,
                    Message = $"Error al eliminar pedido {id}",
                    Data = false
                };
            }
        }
    }
}
