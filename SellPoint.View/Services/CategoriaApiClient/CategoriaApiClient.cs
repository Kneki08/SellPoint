using SellPoint.View.Models.ModelsCategoria;
using System.Net.Http.Json;

namespace SellPoint.View.Services.CategoriaApiClient
{
    public class CategoriaApiClient : ICategoriaApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5271/api/Categoria";

        public CategoriaApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CategoriaDTO>> ObtenerTodosAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/ObtenerTodosAsync");
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<List<CategoriaDTO>>();
                return result ?? new List<CategoriaDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ObtenerTodosAsync] Error: {ex.Message}");
                return new List<CategoriaDTO>();
            }
        }

        public async Task<CategoriaDTO?> ObtenerPorIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<CategoriaDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ObtenerPorIdAsync] Error: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CrearAsync(SaveCategoriaDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/SaveCategoriaDTO", dto);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CrearAsync] Error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ActualizarAsync(UpdateCategoriaDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/UpdateCategoriaDTO", dto);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ActualizarAsync] Error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> EliminarAsync(RemoveCategoriaDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/RemoveCategoriaDTO", dto);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EliminarAsync] Error: {ex.Message}");
                return false;
            }
        }
    }
}
