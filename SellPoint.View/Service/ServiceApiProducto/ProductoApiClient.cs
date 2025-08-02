using SellPoint.Aplication.Dtos.Producto;
using SellPoint.Aplication.Dtos.ProductoDTO;
using System.Net.Http.Json;

namespace SellPoint.View.Service.ServiceApiProducto
{
    public class ProductoApiClient : IProductoApiClient
    {
        private readonly HttpClient _httpClient;

        public ProductoApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductoDTO>> ObtenerTodosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ProductoDTO>>("api/producto");
        }

        public async Task<bool> CrearAsync(SaveProductoDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/producto", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ActualizarAsync(UpdateProductoDTO dto)
        {
            var response = await _httpClient.PutAsJsonAsync("api/producto", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarAsync(RemoveProductoDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/producto/delete", dto);
            return response.IsSuccessStatusCode;
        }
    }
}
