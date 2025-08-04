using Microsoft.Extensions.Configuration;
using SellPoint.View.DTOS.CarritoDTOS;
using SellPoint.View.Models.ModelsCarito;
using SellPoint.View.Models.ModelsCarrito;
using System.Net.Http;
using System.Net.Http.Json;

namespace SellPoint.View.Service.ServiceApiCarrito
{
    public class CarritoApiClient : ICarritoApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public CarritoApiClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiSettings:CarritoBaseUrl"];
            if (string.IsNullOrEmpty(_baseUrl))
            {
                throw new InvalidOperationException("Falta la ruta CarritoBaseUrl en appsettings.json");
            }
        }

        public async Task<List<CarritoModel>> ObtenerTodosAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/ObtenerTodos");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<CarritoResponses>();

            if (result != null && result.data != null)
                return result.data;

            return new List<CarritoModel>();
        }

        public async Task<bool> CrearAsync(SaveCarritoModel model)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/Agregar", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ActualizarAsync(UpdateCarritoModel model)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/Actualizar", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarAsync(RemoveCarritoModel model)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{_baseUrl}/Eliminar")
            {
                Content = JsonContent.Create(model)
            };
            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }
    }
}



