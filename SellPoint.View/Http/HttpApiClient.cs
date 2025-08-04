using SellPoint.View.Models.ModelDetallePedido;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Net.Http.Headers;

namespace SellPoint.View.HTTP
{
    public class HttpApiClient : IHttpApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public HttpApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<ApiResponse<T>> GetAsync<T>(string endpoint)
        {
            try
            {
                var response = await _httpClient.GetAsync(endpoint);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return ApiResponse<T>.CreateError($"Error HTTP {response.StatusCode}: {errorContent}");
                }

                var content = await response.Content.ReadAsStringAsync();

                // Opción para debug: mostrar el JSON recibido
                Console.WriteLine($"JSON recibido: {content}");

                var result = JsonSerializer.Deserialize<T>(content, _jsonOptions);

                if (result == null)
                {
                    return ApiResponse<T>.CreateError("La respuesta deserializada es nula");
                }

                return ApiResponse<T>.CreateSuccess(result);
            }
            catch (JsonException ex)
            {
                return ApiResponse<T>.CreateError($"Error de deserialización: {ex.Message}. Revise la estructura del JSON.");
            }
            catch (Exception ex)
            {
                return ApiResponse<T>.CreateError($"Error inesperado: {ex.Message}");
            }
        }

        public async Task<ApiResponse<T>> PostAsync<T>(string endpoint, object data)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(endpoint, data, _jsonOptions);
                var rawJson = await response.Content.ReadAsStringAsync();

                // Guardar el JSON para análisis
                File.WriteAllText("last_response.json", rawJson);
                Console.WriteLine("JSON recibido: " + rawJson);

                response.EnsureSuccessStatusCode();
                return JsonSerializer.Deserialize<ApiResponse<T>>(rawJson, _jsonOptions);
            }
            catch (Exception ex)
            {
                return new ApiResponse<T>
                {
                    Success = false,
                    Message = $"Error: {ex.Message}"
                };
            }
        }

        private static string NormalizeEndpoint(string endpoint)
        {
            return endpoint.TrimStart('/');
        }
    }
}


