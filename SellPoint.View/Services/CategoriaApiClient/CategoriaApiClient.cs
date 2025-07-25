using SellPoint.View.Models.ModelsCategoria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

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

                var result = await response.Content.ReadFromJsonAsync<OperationResult<List<CategoriaDTO>>>();
                return result?.Data ?? [];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener categorías: {ex.Message}");
                return [];
            }
        }

        public async Task<CategoriaDTO?> ObtenerPorIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}?id={id}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<OperationResult<CategoriaDTO>>();
                    return result?.Data;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener categoría por ID: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CrearAsync(SaveCategoriaDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/SaveCategoriaDTO", dto);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear categoría: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ActualizarAsync(UpdateCategoriaDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/UpdateCategoriaDTO", dto);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar categoría: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> EliminarAsync(RemoveCategoriaDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/RemoveCategoriaDTO", dto);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar categoría: {ex.Message}");
                return false;
            }
        }
    }
}
