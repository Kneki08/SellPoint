using SellPoint.Aplication.Dtos.Carrito;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SellPoint.View.Service.ServiceApiCarrito
{
    public class CarritoApiClient : ICarritoApiClient
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7121/api/Carrito";

        public CarritoApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ObtenerCarritoDTO>> ObtenerTodosAsync()
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/ObtenerTodos");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<ObtenerCarritoDTO>>();
        }

        public async Task<bool> CrearAsync(SaveCarritoDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/Agregar", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ActualizarAsync(UpdateCarritoDTO dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/Actualizar", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarAsync(RemoveCarritoDTO dto)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{BaseUrl}/Eliminar")
            {
                Content = JsonContent.Create(dto)
            };
            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }
    }
}

