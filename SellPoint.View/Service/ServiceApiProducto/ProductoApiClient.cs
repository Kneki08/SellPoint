using Microsoft.Extensions.Configuration;
using SellPoint.View.DTOS.ProductoDTOS;
using SellPoint.View.Models.ModelsProducto;
using SellPoint.View.Models.ModelsProducto.SellPoint.View.Models.ModelsProducto;
using System.Net.Http;
using System.Net.Http.Json;

namespace SellPoint.View.Service.ServiceApiProducto
{
    public class ProductoApiClient : IProductoApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ProductoApiClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiSettings:ProductoBaseUrl"];
            if (string.IsNullOrEmpty(_baseUrl))
            {
                throw new InvalidOperationException("Falta la ruta ProductoBaseUrl en appsettings.json");
            }
        }

        public async Task<List<ProductoModel>> ObtenerTodosAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/ObtenerTodos");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ProductoResponses>();

            if (result != null && result.data != null)
                return result.data;

            return new List<ProductoModel>();
        }

        public async Task<bool> CrearAsync(SaveProductoModel model)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/Agregar", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ActualizarAsync(UpdateProductoModel model)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/Actualizar", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarAsync(RemoveProductoModel model)
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

