using Microsoft.Extensions.Logging;
using SellPoint.View.Models.ModelDetallePedido;
using SellPoint.View.Models.ModelDetallePedido.Dtos;
using SellPoint.View.HTTP;
using SellPoint.View.Validations;
using System.Net;

namespace SellPoint.View.Service
{
    public class DetallePedidoService : IDetallePedidoService
    {
        private readonly IHttpApiClient _httpClient;
        private readonly ILogger<DetallePedidoService> _logger;

        public DetallePedidoService(IHttpApiClient httpClient, ILogger<DetallePedidoService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<ApiResponse<IEnumerable<DetalleDto>>> GetAllAsync()
        {
            const string endpoint = "DetallePedido/ObtenerTodosAsync";
            _logger.LogInformation("Obteniendo todos los detalles de pedido. Endpoint: {Endpoint}", endpoint);

            try
            {
                var response = await _httpClient.GetAsync<DetallePedidoModelResponse>(endpoint);

                if (!response.Success)
                {
                    _logger.LogWarning("Fallo al obtener detalles. Mensaje: {Message}", response.Message);
                    return ApiResponse<IEnumerable<DetalleDto>>.CreateError(response.Message ?? "Error al obtener datos");
                }

                if (response.Data?.data == null)
                {
                    _logger.LogInformation("No se encontraron detalles de pedido");
                    return ApiResponse<IEnumerable<DetalleDto>>.CreateSuccess(new List<DetalleDto>());
                }

                var listaDetalles = response.Data.data
                    .Where(model => model != null)
                    .Select(model => new DetalleDto
                    {
                        Id = model.Id,
                        PedidoId = model.PedidoId,
                        ProductoId = model.ProductoId,
                        Cantidad = model.Cantidad,
                        PrecioUnitario = model.PrecioUnitario
                    }).ToList();

                _logger.LogInformation("Se obtuvieron {Count} detalles de pedido exitosamente", listaDetalles.Count);
                return ApiResponse<IEnumerable<DetalleDto>>.CreateSuccess(listaDetalles);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error HTTP al obtener detalles. Status: {StatusCode}", ex.StatusCode);
                return ApiResponse<IEnumerable<DetalleDto>>.CreateError($"Error de conexión: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener detalles");
                return ApiResponse<IEnumerable<DetalleDto>>.CreateError($"Error al obtener los detalles: {ex.Message}");
            }
        }

        public async Task<ApiResponse<DetalleDto>> GetByIdAsync(int id)
        {
            _logger.LogInformation("Obteniendo detalle por ID: {Id}", id);

            if (!DetallePedidoValidator.ValidateId(id, out var error))
            {
                _logger.LogWarning("ID de detalle no válido: {Error}", error);
                return ApiResponse<DetalleDto>.CreateError(error);
            }

            try
            {
                var response = await _httpClient.GetAsync<DetallePedidoModelResponseSingle>($"DetallePedido/{id}");

                if (response == null || response.Data == null)
                {
                    _logger.LogWarning("No se encontró detalle con ID: {Id}", id);
                    return ApiResponse<DetalleDto>.CreateError("No se encontró el detalle con el ID especificado");
                }

                var detalle = new DetalleDto
                {
                    Id = response.Data.data.Id,
                    PedidoId = response.Data.data.PedidoId,
                    ProductoId = response.Data.data.ProductoId,
                    Cantidad = response.Data.data.Cantidad,
                    PrecioUnitario = response.Data.data.PrecioUnitario
                };

                _logger.LogInformation("Detalle con ID {Id} obtenido exitosamente", id);
                return ApiResponse<DetalleDto>.CreateSuccess(detalle);
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                _logger.LogWarning("Detalle no encontrado. ID: {Id}", id);
                return ApiResponse<DetalleDto>.CreateError($"Detalle con ID {id} no encontrado");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener detalle por ID");
                return ApiResponse<DetalleDto>.CreateError($"Error al obtener el detalle: {ex.Message}");
            }
        }

        public async Task<ApiResponse<bool>> CreateAsync(SaveDto dto)
        {
            _logger.LogInformation("Intentando crear nuevo detalle de pedido");

            if (dto == null)
            {
                _logger.LogError("DTO de creación no puede ser nulo");
                return ApiResponse<bool>.CreateError("El DTO no puede ser nulo");
            }

            if (!DetallePedidoValidator.Validate(dto, out var errors))
            {
                _logger.LogWarning("Validación fallida para nuevo detalle. Errores: {Errors}", string.Join(", ", errors));
                return ApiResponse<bool>.CreateError(string.Join(", ", errors));
            }

            try
            {
                _logger.LogDebug("Enviando solicitud para crear detalle. DTO: {@Dto}", dto);
                var response = await _httpClient.PostAsync<ApiResponse<bool>>(
                    "DetallePedido/SaveDetallePedidoDTO", dto);

                if (!response.Success)
                {
                    _logger.LogError("Error al crear detalle. Respuesta: {@Response}", response);
                    return ApiResponse<bool>.CreateError(response.Message ?? "Error al crear el detalle");
                }

                _logger.LogInformation("Detalle creado exitosamente");
                return ApiResponse<bool>.CreateSuccess(true, "Detalle creado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear detalle");
                return ApiResponse<bool>.CreateError($"Error al crear el detalle: {ex.Message}");
            }
        }

        public async Task<ApiResponse<bool>> UpdateAsync(UpdateDto dto)
        {
            _logger.LogInformation("Intentando actualizar detalle. ID: {Id}", dto?.Id);

            if (dto == null)
            {
                _logger.LogError("DTO de actualización no puede ser nulo");
                return ApiResponse<bool>.CreateError("El DTO no puede ser nulo");
            }

            try
            {
                _logger.LogDebug("Enviando solicitud para actualizar detalle. DTO: {@Dto}", dto);
                var response = await _httpClient.PostAsync<ApiResponse<bool>>(
                    "DetallePedido/UpdateDetallePedidoDTO", dto);

                if (!response.Success)
                {
                    _logger.LogError("Error al actualizar detalle. Respuesta: {@Response}", response);
                    return ApiResponse<bool>.CreateError(response.Message ?? "Error al actualizar el detalle");
                }

                _logger.LogInformation("Detalle {Id} actualizado exitosamente", dto.Id);
                return ApiResponse<bool>.CreateSuccess(true, "Detalle actualizado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar detalle");
                return ApiResponse<bool>.CreateError($"Error al actualizar el detalle: {ex.Message}");
            }
        }

        public async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            _logger.LogInformation("Intentando eliminar detalle. ID: {Id}", id);

            if (!DetallePedidoValidator.ValidateId(id, out var error))
            {
                _logger.LogWarning("ID para eliminación no válido: {Error}", error);
                return ApiResponse<bool>.CreateError(error);
            }

            try
            {
                _logger.LogDebug("Enviando solicitud para eliminar detalle. ID: {Id}", id);
                var response = await _httpClient.PostAsync<ApiResponse<bool>>(
                    "DetallePedido/RemoveDetallePedidoDTO", new { Id = id });

                if (!response.Success)
                {
                    _logger.LogError("Error al eliminar detalle. Respuesta: {@Response}", response);
                    return ApiResponse<bool>.CreateError(response.Message ?? "Error al eliminar el detalle");
                }

                _logger.LogInformation("Detalle {Id} eliminado exitosamente", id);
                return ApiResponse<bool>.CreateSuccess(true, "Detalle eliminado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar detalle");
                return ApiResponse<bool>.CreateError($"Error al eliminar el detalle: {ex.Message}");
            }
        }
    }

}
