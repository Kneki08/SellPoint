using SellPoint.View.Models.ModelsCupon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.View.Services.CuponApiClient
{
    public class CuponApiClient : ICuponApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5271/api/Cupon";

        public CuponApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CuponDTO>> ObtenerTodosAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/ObtenerTodosAsync");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<IEnumerable<CuponDTO>>() ?? [];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener cupones: {ex.Message}");
                return [];
            }
        }

        public async Task<CuponDTO?> ObtenerPorIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}?id={id}");
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadFromJsonAsync<CuponDTO>();

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener cupón por ID: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CrearAsync(SaveCuponDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/SaveCuponDTO", dto);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear cupón: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ActualizarAsync(UpdateCuponDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/UpdateCuponDTO", dto);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar cupón: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> EliminarAsync(RemoveCuponDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/RemoveCuponDTO", dto);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar cupón: {ex.Message}");
                return false;
            }
        }
    }


}
