using SellPoint.Aplication.Dtos.DetallePedido;
using SellPoint.View.Base;
using SellPoint.View.Models.ModelDetallePedido;
using SellPoint.View.Service.DetallePedidoClient.Contract;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace SellPoint.View.Service.DetallePedidoClient.Implement
{
    public class DetallePedidoApiClient : IDetallePedidoApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5271/api/";
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public DetallePedidoApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<List<DetallePedidoDTO>>> ObtenerTodosAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}DetallePedido/ObtenerTodosAsync");
                if (!response.IsSuccessStatusCode)
                {
                    return ApiResponse<List<DetallePedidoDTO>>.CreateError(
                        $"Error al obtener datos: {response.StatusCode}"
                    );
                }

                var content = await response.Content.ReadAsStringAsync();
                var wrapper = JsonSerializer.Deserialize<DetallePedidoModelResponse>(content, _jsonOptions);

                var result = new List<DetallePedidoDTO>();
                foreach (var item in wrapper?.data ?? new List<DetallePedidoModel>())
                {
                    result.Add(new DetallePedidoDTO
                    {
                        Id = item.Id,
                        Cantidad = item.Cantidad,
                        PrecioUnitario = item.PrecioUnitario,
                        PedidoId = item.PedidoId,
                        ProductoId = item.ProductoId
                    });
                }

                return ApiResponse<List<DetallePedidoDTO>>.CreateSuccess(result);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<DetallePedidoDTO>>.CreateError(
                    $"Error crítico al obtener detalles: {ex.Message}"
                );
            }
        }

        public async Task<ApiResponse<DetallePedidoDTO>> ObtenerPorIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}{id}");
                if (!response.IsSuccessStatusCode)
                {
                    return ApiResponse<DetallePedidoDTO>.CreateError(
                        $"Error al obtener detalle: {response.StatusCode}"
                    );
                }

                var content = await response.Content.ReadAsStringAsync();
                var wrapper = JsonSerializer.Deserialize<DetallePedidoModelResponseSingle>(content, _jsonOptions);

                var item = wrapper?.data;
                if (item == null)
                {
                    return ApiResponse<DetallePedidoDTO>.CreateError("Detalle no encontrado");
                }

                return ApiResponse<DetallePedidoDTO>.CreateSuccess(new DetallePedidoDTO
                {
                    Id = item.Id,
                    Cantidad = item.Cantidad,
                    PrecioUnitario = item.PrecioUnitario,
                    PedidoId = item.PedidoId,
                    ProductoId = item.ProductoId
                });
            }
            catch (Exception ex)
            {
                return ApiResponse<DetallePedidoDTO>.CreateError(
                    $"Error crítico al obtener detalle: {ex.Message}"
                );
            }
        }

        public async Task<ApiResponse<bool>> CrearAsync(SaveDetallePedidoDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(
                    $"{_baseUrl}DetallePedido/SaveDetallePedidoDTO",
                    dto,
                    _jsonOptions
                );

                return response.IsSuccessStatusCode
                    ? ApiResponse<bool>.CreateSuccess(true, "Detalle creado exitosamente")
                    : ApiResponse<bool>.CreateError($"Error al crear: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.CreateError($"Error crítico al crear: {ex.Message}");
            }
        }

        public async Task<ApiResponse<bool>> ActualizarAsync(UpdateDetallePedidoDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(
                    $"{_baseUrl}DetallePedido/UpdateDetallePedidoDTO",
                    dto,
                    _jsonOptions
                );

                return response.IsSuccessStatusCode
                    ? ApiResponse<bool>.CreateSuccess(true, "Detalle actualizado exitosamente")
                    : ApiResponse<bool>.CreateError($"Error al actualizar: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.CreateError($"Error crítico al actualizar: {ex.Message}");
            }
        }

        public async Task<ApiResponse<bool>> EliminarAsync(RemoveDetallePedidoDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(
                    $"{_baseUrl}DetallePedido/RemoveDetallePedidoDTO",
                    dto,
                    _jsonOptions
                );

                return response.IsSuccessStatusCode
                    ? ApiResponse<bool>.CreateSuccess(true, "Detalle eliminado exitosamente")
                    : ApiResponse<bool>.CreateError($"Error al eliminar: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.CreateError($"Error crítico al eliminar: {ex.Message}");
            }
        }

        public async Task<ApiResponse<List<DetallePedidoDTO>>> ObtenerPorIdPedidoAsync(int idPedido)
        {
            var todosLosDetalles = await ObtenerTodosAsync();
            if (!todosLosDetalles.Success)
            {
                return ApiResponse<List<DetallePedidoDTO>>.CreateError(todosLosDetalles.Message);
            }

            var detallesFiltrados = todosLosDetalles.Data
                .Where(d => d.PedidoId == idPedido)
                .ToList();

            return ApiResponse<List<DetallePedidoDTO>>.CreateSuccess(detallesFiltrados);
        }
    }
}
