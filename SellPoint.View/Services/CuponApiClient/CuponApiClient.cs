using SellPoint.Aplication.Dtos.Categoria;
using SellPoint.Aplication.Dtos.Cupon;
using SellPoint.View.Models.ModelsCupon;
using System.Net.Http.Json;
using System.Text.Json;

namespace SellPoint.View.Services.CuponApiClient
{
    public class CuponApiClient : ICuponApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ICuponMapper _mapper;
        private readonly JsonSerializerOptions _jsonOptions;

        public CuponApiClient(HttpClient httpClient, ICuponMapper mapper, JsonSerializerOptions jsonOptions)
        {
            _httpClient = httpClient;
            _mapper = mapper;
            _jsonOptions = jsonOptions;
        }

        public async Task<IEnumerable<CuponDTO>> ObtenerTodosAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("Cupon/ObtenerTodosAsync");
                response.EnsureSuccessStatusCode();

                var wrapper = await response.Content.ReadFromJsonAsync<CuponModelResponse>(_jsonOptions);
                return _mapper.Convert(wrapper?.data ?? Enumerable.Empty<CuponModel>());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener cupones: {ex.Message}");
                return new List<CuponDTO>();
            }
        }

        public async Task<CuponDTO?> ObtenerPorIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"Cupon/{id}");
                if (!response.IsSuccessStatusCode) return null;

                var wrapper = await response.Content.ReadFromJsonAsync<CuponModelResponseSingle>(_jsonOptions);
                return wrapper?.data != null ? _mapper.Convert(wrapper.data) : null;
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
                var response = await _httpClient.PostAsJsonAsync("Cupon", dto, _jsonOptions);
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
                var response = await _httpClient.PutAsJsonAsync("Cupon", dto, _jsonOptions);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar cupón: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> EliminarAsync(RemoveCuponDTIO dto)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, "Cupon")
                {
                    Content = JsonContent.Create(dto, options: _jsonOptions)
                };

                var response = await _httpClient.SendAsync(request);
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
