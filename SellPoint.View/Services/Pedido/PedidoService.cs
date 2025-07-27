using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using SellPoint.View.Models;
using SellPoint.View.Models.Pedido;

namespace SellPoint.View.Services.Pedido
{
    /// <summary>
    /// Servicio concreto que consume la API de Pedido mediante HttpClient.
    /// </summary>
    public class PedidoService : IPedidoService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public PedidoService(string baseUrl)
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl;
        }

        public async Task<ApiResponse<List<PedidoDTO>>> ObtenerTodosAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(_baseUrl);
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<List<PedidoDTO>>>();
                if (response.IsSuccessStatusCode && apiResponse != null)
                {
                    return apiResponse;
                }
                return new ApiResponse<List<PedidoDTO>>
                {
                    IsSuccess = false,
                    Message = apiResponse?.Message ?? "Error al obtener los pedidos.",
                    Data = new List<PedidoDTO>()
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<PedidoDTO>>
                {
                    IsSuccess = false,
                    Message = $"Excepción al obtener pedidos: {ex.Message}",
                    Data = new List<PedidoDTO>()
                };
            }
        }

        public async Task<ApiResponse<PedidoDTO>> ObtenerPorIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<PedidoDTO>>();
                if (response.IsSuccessStatusCode && apiResponse != null)
                {
                    return apiResponse;
                }
                return new ApiResponse<PedidoDTO>
                {
                    IsSuccess = false,
                    Message = apiResponse?.Message ?? "No se encontró el pedido.",
                    Data = default
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<PedidoDTO>
                {
                    IsSuccess = false,
                    Message = $"Excepción al buscar pedido: {ex.Message}",
                    Data = default
                };
            }
        }

        public async Task<ApiResponse<bool>> AgregarAsync(SavePedidoDTO dto)
        {
            try
            {
                var json = JsonSerializer.Serialize(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(_baseUrl, content);
                var resultado = await response.Content.ReadFromJsonAsync<ApiResponse<bool>>();

                if (response.IsSuccessStatusCode && resultado != null)
                {
                    return resultado;
                }

                return new ApiResponse<bool>
                {
                    IsSuccess = false,
                    Message = resultado?.Message ?? "Error desconocido al agregar el pedido.",
                    Data = false
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>
                {
                    IsSuccess = false,
                    Message = $"Excepción al agregar: {ex.Message}",
                    Data = false
                };
            }
        }

        public async Task<ApiResponse<bool>> ActualizarAsync(UpdatePedidoDTO dto)
        {
            try
            {
                var json = JsonSerializer.Serialize(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var url = $"{_baseUrl}/{dto.Id}";
                var response = await _httpClient.PutAsync(url, content);
                var resultado = await response.Content.ReadFromJsonAsync<ApiResponse<bool>>();

                if (response.IsSuccessStatusCode && resultado != null)
                {
                    return resultado;
                }

                return new ApiResponse<bool>
                {
                    IsSuccess = false,
                    Message = resultado?.Message ?? "Error desconocido al actualizar el pedido.",
                    Data = false
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>
                {
                    IsSuccess = false,
                    Message = $"Excepción al actualizar: {ex.Message}",
                    Data = false
                };
            }
        }

        public async Task<ApiResponse<bool>> EliminarAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
                var resultado = await response.Content.ReadFromJsonAsync<ApiResponse<bool>>();

                if (response.IsSuccessStatusCode && resultado != null)
                {
                    return resultado;
                }

                return new ApiResponse<bool>
                {
                    IsSuccess = false,
                    Message = resultado?.Message ?? "Error desconocido al eliminar el pedido.",
                    Data = false
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>
                {
                    IsSuccess = false,
                    Message = $"Excepción al eliminar: {ex.Message}",
                    Data = false
                };
            }
        }
    }
}