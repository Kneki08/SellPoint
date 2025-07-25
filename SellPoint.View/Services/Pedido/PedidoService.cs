using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using SellPoint.View.Models;
using SellPoint.View.Dtos.Pedido;

namespace SellPoint.View.Services.Pedido
{
    /// <summary>
    /// Servicio concreto que consume la API de Pedido mediante HttpClient.
    /// </summary>
    public class PedidoService : IPedidoService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://localhost:5271/api/Pedido";

        public PedidoService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<PedidoDTO>> ObtenerTodosAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(BaseUrl);
                response.EnsureSuccessStatusCode();

                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<List<PedidoDTO>>>();
                return apiResponse?.Data ?? new List<PedidoDTO>();
            }
            catch
            {
                return new List<PedidoDTO>();
            }
        }

        public async Task<PedidoDTO?> ObtenerPorIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}/{id}");
                response.EnsureSuccessStatusCode();

                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<PedidoDTO>>();
                return apiResponse?.Data;
            }
            catch
            {
                return null;
            }
        }

        public async Task<(bool exito, string mensaje)> AgregarAsync(SavePedidoDTO dto)
        {
            try
            {
                var json = JsonSerializer.Serialize(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(BaseUrl, content);
                var resultado = await response.Content.ReadFromJsonAsync<ApiResponse<bool>>();

                if (response.IsSuccessStatusCode && resultado != null)
                {
                    return (resultado.IsSuccess, resultado.Message);
                }

                return (false, resultado?.Message ?? "Error desconocido al agregar el pedido.");
            }
            catch (Exception ex)
            {
                return (false, $"Excepción al agregar: {ex.Message}");
            }
        }

        public async Task<(bool exito, string mensaje)> ActualizarAsync(UpdatePedidoDTO dto)
        {
            try
            {
                var json = JsonSerializer.Serialize(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var url = $"{BaseUrl}/{dto.Id}";
                var response = await _httpClient.PutAsync(url, content);
                var resultado = await response.Content.ReadFromJsonAsync<ApiResponse<bool>>();

                if (response.IsSuccessStatusCode && resultado != null)
                {
                    return (resultado.IsSuccess, resultado.Message);
                }

                return (false, resultado?.Message ?? "Error desconocido al actualizar el pedido.");
            }
            catch (Exception ex)
            {
                return (false, $"Excepción al actualizar: {ex.Message}");
            }
        }

        public async Task<(bool exito, string mensaje)> EliminarAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
                var resultado = await response.Content.ReadFromJsonAsync<ApiResponse<bool>>();

                if (response.IsSuccessStatusCode && resultado != null)
                {
                    return (resultado.IsSuccess, resultado.Message);
                }

                return (false, resultado?.Message ?? "Error desconocido al eliminar el pedido.");
            }
            catch (Exception ex)
            {
                return (false, $"Excepción al eliminar: {ex.Message}");
            }
        }
    }
}