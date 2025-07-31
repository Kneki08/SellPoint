using SellPoint.Aplication.Dtos.Categoria;
using SellPoint.View.Models.ModelsCategoria;
using System.Net.Http.Json;
using System.Text.Json;

namespace SellPoint.View.Services.CategoriaApiClient
{
    public class CategoriaApiClient : ICategoriaApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ICategoriaMapper _mapper;
        private readonly JsonSerializerOptions _jsonOptions;

        public CategoriaApiClient(HttpClient httpClient, ICategoriaMapper mapper, JsonSerializerOptions jsonOptions)
        {
            _httpClient = httpClient;
            _mapper = mapper;
            _jsonOptions = jsonOptions;
        }

        public async Task<IEnumerable<CategoriaDTO>> ObtenerTodosAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("Categoria/ObtenerTodosAsync");
                response.EnsureSuccessStatusCode();

                var wrapper = await response.Content.ReadFromJsonAsync<CategoriaModelResponse>(_jsonOptions);
                return _mapper.Convert(wrapper?.data ?? Enumerable.Empty<CategoriaModel>());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener categorías: {ex.Message}");
                return new List<CategoriaDTO>();
            }
        }

        public async Task<CategoriaDTO?> ObtenerPorIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"Categoria/{id}");
                if (!response.IsSuccessStatusCode) return null;

                var wrapper = await response.Content.ReadFromJsonAsync<CategoriaModelResponseSingle>(_jsonOptions);
                return wrapper?.data != null ? _mapper.Convert(wrapper.data) : null;
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
                var response = await _httpClient.PostAsJsonAsync("Categoria/SaveCategoriaDTO", dto, _jsonOptions);
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
                var response = await _httpClient.PutAsJsonAsync("Categoria/UpdateCategoriaDTO", dto, _jsonOptions);
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
                var response = await _httpClient.PostAsJsonAsync("Categoria/RemoveCategoriaDTO", dto, _jsonOptions);
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


