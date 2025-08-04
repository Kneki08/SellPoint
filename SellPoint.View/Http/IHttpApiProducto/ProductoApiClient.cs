using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SellPoint.View.DTOS.ProductoDTOS;
using SellPoint.View.Http.ServiceApiProducto;
using SellPoint.View.Models.ModelsProducto;
using SellPoint.View.Models.ModelsProducto.SellPoint.View.Models.ModelsProducto;
using System.Net.Http;
using System.Net.Http.Json;

namespace SellPoint.View.Service.ServiceApiProducto
{
    public class ProductoApiClient : BaseApiClient, IProductoApiClient
    {
        public ProductoApiClient(HttpClient httpClient, IConfiguration configuration, ILogger<ProductoApiClient> logger)
            : base(httpClient, configuration, logger, "ApiSettings:BaseUrl") { } 

        public async Task<List<ProductoModel>> ObtenerTodosAsync()
        {
            var result = await GetAsync<ProductoResponses>("/ObtenerTodos");
            return result?.data ?? new List<ProductoModel>();
        }

        public Task<bool> CrearAsync(SaveProductoModel model)
            => PostAsync("/Agregar", model);

        public Task<bool> ActualizarAsync(UpdateProductoModel model)
            => PutAsync("/Actualizar", model);

        public Task<bool> EliminarAsync(RemoveProductoModel model)
            => DeleteAsync("/Eliminar", model);
    }
}


