using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json;


namespace SellPoint.View.Services
{
    public class HttpServiceBase
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly ILogger<HttpServiceBase> _logger;

        public HttpServiceBase(HttpClient httpClient, JsonSerializerOptions jsonOptions, ILogger<HttpServiceBase> logger)
        {
            _httpClient = httpClient;
            _jsonOptions = jsonOptions;
            _logger = logger;
        }

        public async Task<T?> GetAsync<T>(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<T>(_jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en GET: {url}");
                return default;
            }
        }

        public async Task<T?> PostAsync<T>(string url, object body)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(url, body, _jsonOptions);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<T>(_jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en POST: {url}");
                return default;
            }
        }

        public async Task<T?> PutAsync<T>(string url, object body)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(url, body, _jsonOptions);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<T>(_jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en PUT: {url}");
                return default;
            }
        }

        public async Task<bool> DeleteAsync(string url, object body)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, url)
                {
                    Content = JsonContent.Create(body, options: _jsonOptions)
                };

                var response = await _httpClient.SendAsync(request);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en DELETE: {url}");
                return false;
            }
        }
    }
}
