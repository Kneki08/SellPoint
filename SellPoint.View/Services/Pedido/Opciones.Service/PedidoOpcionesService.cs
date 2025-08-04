using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SellPoint.View.Services.Pedido
{
    public class PedidoOpcionesService : IPedidoOpcionesService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PedidoOpcionesService> _logger;

        public PedidoOpcionesService(HttpClient httpClient, ILogger<PedidoOpcionesService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<int>> ObtenerUsuariosAsync()
        {
            return await GetFromApiAsync<List<int>>("pedidoopciones/usuarios") ?? new List<int>();
        }

        public async Task<List<int>> ObtenerDireccionesAsync()
        {
            return await GetFromApiAsync<List<int>>("pedidoopciones/direcciones") ?? new List<int>();
        }

        public async Task<List<string>> ObtenerMetodosPagoAsync()
        {
            return await GetFromApiAsync<List<string>>("pedidoopciones/metodos-pago") ?? new List<string>();
        }

        public async Task<List<string>> ObtenerEstadosAsync()
        {
            return await GetFromApiAsync<List<string>>("pedidoopciones/estados-pedido") ?? new List<string>();
        }

        private async Task<T?> GetFromApiAsync<T>(string endpoint)
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<T>(endpoint);
                if (result == null)
                {
                    _logger.LogWarning("El endpoint {Endpoint} devolvió null", endpoint);
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener datos desde el endpoint {Endpoint}", endpoint);
                return default;
            }
        }
    }
}
