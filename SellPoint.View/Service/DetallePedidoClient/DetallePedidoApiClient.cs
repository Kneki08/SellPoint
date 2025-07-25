using SellPoint.Aplication.Dtos.DetallePedido;
using SellPoint.View.Models.ModelDetallePedido;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace SellPoint.View.Service.DetallePedidoClient
{
    public class DetallePedidoApiClient : IDetallePedidoApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5271/api/";
        public DetallePedidoApiClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<DetallePedidoDTO>> ObtenerTodosAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}DetallePedido/ObtenerTodosAsync");

            if (!response.IsSuccessStatusCode)
                return new List<DetallePedidoDTO>();

            var content = await response.Content.ReadAsStringAsync();

            var wrapper = JsonSerializer.Deserialize<DetallePedidoModelResponse>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

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

            return result;
        }

        public async Task<DetallePedidoDTO> ObtenerPorIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}DetallePedido/{id}");

            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();
            var wrapper = JsonSerializer.Deserialize<DetallePedidoModelResponseSingle>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var item = wrapper?.data;
            if (item == null)
                return null;

            return new DetallePedidoDTO
            {
                Id = item.Id,
                Cantidad = item.Cantidad,
                PrecioUnitario = item.PrecioUnitario,
                PedidoId = item.PedidoId,
                ProductoId = item.ProductoId
            };
        }

        public async Task<bool> CrearAsync(SaveDetallePedidoDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}DetallePedido/SaveDetallePedidoDTO", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ActualizarAsync(UpdateDetallePedidoDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}DetallePedido/UpdateDetallePedidoDTO", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarAsync(RemoveDetallePedidoDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}DetallePedido/RemoveDetallePedidoDTO", dto);
            return response.IsSuccessStatusCode;
        }
    }
}
