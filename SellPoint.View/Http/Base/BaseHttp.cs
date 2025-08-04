using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace SellPoint.View.Service
{
    public abstract class BaseApiClient
    {
        protected readonly HttpClient _httpClient;
        protected readonly string _baseUrl;
        protected readonly ILogger _logger;

        protected BaseApiClient(HttpClient httpClient, IConfiguration configuration, ILogger logger, string configKey)
        {
            _httpClient = httpClient;
            _logger = logger;

            _baseUrl = configuration[configKey] ?? throw new InvalidOperationException($"Falta la ruta {configKey} en appsettings.json");

            _logger.LogInformation("BaseApiClient inicializado para {ConfigKey} con URL base {BaseUrl}", configKey, _baseUrl);
        }

        protected async Task<T?> GetAsync<T>(string endpoint)
        {
            try
            {
                _logger.LogInformation("GET -> {Url}", $"{_baseUrl}{endpoint}");
                var response = await _httpClient.GetAsync($"{_baseUrl}{endpoint}");
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<T>();
                _logger.LogInformation("GET completado correctamente");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GET {Url}", $"{_baseUrl}{endpoint}");
                throw;
            }
        }

        protected async Task<bool> PostAsync<T>(string endpoint, T data)
        {
            try
            {
                _logger.LogInformation("POST -> {Url} con datos: {@Data}", $"{_baseUrl}{endpoint}", data);
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}{endpoint}", data);
                _logger.LogInformation("POST completado con estado {StatusCode}", response.StatusCode);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en POST {Url}", $"{_baseUrl}{endpoint}");
                throw;
            }
        }

        protected async Task<bool> PutAsync<T>(string endpoint, T data)
        {
            try
            {
                _logger.LogInformation("PUT -> {Url} con datos: {@Data}", $"{_baseUrl}{endpoint}", data);
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}{endpoint}", data);
                _logger.LogInformation("PUT completado con estado {StatusCode}", response.StatusCode);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en PUT {Url}", $"{_baseUrl}{endpoint}");
                throw;
            }
        }

        protected async Task<bool> DeleteAsync<T>(string endpoint, T data)
        {
            try
            {
                _logger.LogInformation("DELETE -> {Url} con datos: {@Data}", $"{_baseUrl}{endpoint}", data);
                var request = new HttpRequestMessage(HttpMethod.Delete, $"{_baseUrl}{endpoint}")
                {
                    Content = JsonContent.Create(data)
                };
                var response = await _httpClient.SendAsync(request);
                _logger.LogInformation("DELETE completado con estado {StatusCode}", response.StatusCode);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en DELETE {Url}", $"{_baseUrl}{endpoint}");
                throw;
            }
        }
    }
}
