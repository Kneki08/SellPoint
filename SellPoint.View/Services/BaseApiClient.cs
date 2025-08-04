using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using SellPoint.View.Models;

namespace SellPoint.View.Services
{
    /// <summary>
    /// Clase base reutilizable para todos los clientes API.
    /// Centraliza manejo de respuestas HTTP y errores.
    /// </summary>
    public abstract class BaseApiClient
    {
        private readonly JsonSerializerOptions _jsonOptions;

        protected BaseApiClient(JsonSerializerOptions jsonOptions)
        {
            _jsonOptions = jsonOptions;
        }

        /// <summary>
        /// Deserializa una respuesta HTTP a un ApiResponse<T>.
        /// </summary>
        protected async Task<ApiResponse<T>> LeerRespuesta<T>(HttpResponseMessage response, string mensajeError)
        {
            var resultado = await response.Content.ReadFromJsonAsync<ApiResponse<T>>(_jsonOptions);
            if (response.IsSuccessStatusCode && resultado != null)
                return resultado;

            return new ApiResponse<T>
            {
                IsSuccess = false,
                Message = resultado?.Message ?? mensajeError,
                Data = default
            };
        }

        /// <summary>
        /// Crea una respuesta de error estándar.
        /// </summary>
        protected ApiResponse<T> Error<T>(string mensaje)
        {
            return new ApiResponse<T>
            {
                IsSuccess = false,
                Message = mensaje,
                Data = typeof(T) == typeof(bool) ? (T)(object)false : default
            };
        }
    }
}