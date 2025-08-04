using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using SellPoint.View.Models;
using SellPoint.View.Models.Pedido;

namespace SellPoint.View.Services.Pedido.Api.Client
{
    public class PedidoApiClient : BaseApiClient, IPedidoApiClient
    {
        private readonly HttpClient _httpClient;
        private const string Endpoint = "Pedido";

        public PedidoApiClient(HttpClient httpClient, JsonSerializerOptions jsonOptions)
            : base(jsonOptions)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<List<PedidoDTO>>> ObtenerTodosAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(Endpoint);
                return await LeerRespuesta<List<PedidoDTO>>(response, "Error al obtener los pedidos.");
            }
            catch (Exception ex)
            {
                return Error<List<PedidoDTO>>($"Excepción al obtener pedidos: {ex.Message}");
            }
        }

        public async Task<ApiResponse<PedidoDTO>> ObtenerPorIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{Endpoint}/{id}");
                return await LeerRespuesta<PedidoDTO>(response, "No se encontró el pedido.");
            }
            catch (Exception ex)
            {
                return Error<PedidoDTO>($"Excepción al buscar pedido: {ex.Message}");
            }
        }

        public async Task<ApiResponse<bool>> AgregarAsync(SavePedidoDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(Endpoint, dto);
                return await LeerRespuesta<bool>(response, "Error desconocido al agregar el pedido.");
            }
            catch (Exception ex)
            {
                return Error<bool>($"Excepción al agregar: {ex.Message}");
            }
        }

        public async Task<ApiResponse<bool>> ActualizarAsync(UpdatePedidoDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{dto.Id}", dto);
                return await LeerRespuesta<bool>(response, "Error desconocido al actualizar el pedido.");
            }
            catch (Exception ex)
            {
                return Error<bool>($"Excepción al actualizar: {ex.Message}");
            }
        }

        public async Task<ApiResponse<bool>> EliminarAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{Endpoint}/{id}");
                return await LeerRespuesta<bool>(response, "Error desconocido al eliminar el pedido.");
            }
            catch (Exception ex)
            {
                return Error<bool>($"Excepción al eliminar: {ex.Message}");
            }
        }
    }
}