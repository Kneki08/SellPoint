using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SellPoint.View.DTOS.CarritoDTOS;
using SellPoint.View.Models.ModelsCarito;
using SellPoint.View.Models.ModelsCarrito;
using System.Net.Http;
using System.Net.Http.Json;

namespace SellPoint.View.Service.ServiceApiCarrito
{
    public class CarritoApiClient : BaseApiClient, ICarritoApiClient
    {
        public CarritoApiClient(HttpClient httpClient, IConfiguration configuration, ILogger<CarritoApiClient> logger)
            : base(httpClient, configuration, logger, "ApiSettings:BaseUrl") { }

        public async Task<List<CarritoModel>> ObtenerTodosAsync()
        {
            var result = await GetAsync<CarritoResponses>("/ObtenerTodos");
            return result?.data ?? new List<CarritoModel>();
        }

        public Task<bool> CrearAsync(SaveCarritoModel model)
            => PostAsync("/Agregar", model);

        public Task<bool> ActualizarAsync(UpdateCarritoModel model)
            => PutAsync("/Actualizar", model);

        public Task<bool> EliminarAsync(RemoveCarritoModel model)
            => DeleteAsync("/Eliminar", model);
    }
}




