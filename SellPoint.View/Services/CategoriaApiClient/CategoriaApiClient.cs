using SellPoint.View.Mappers.Categoria;
using SellPoint.View.Models.ModelsCategoria;

namespace SellPoint.View.Services.CategoriaApiClient
{
    public class CategoriaApiClient : ICategoriaApiClient
    {
        private readonly HttpServiceBase _httpService;
        private readonly ICategoriaMapper _mapper;

        public CategoriaApiClient(HttpServiceBase httpService, ICategoriaMapper mapper)
        {
            _httpService = httpService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoriaModel>> ObtenerTodosAsync()
        {
            var result = await _httpService.GetAsync<CategoriaModelResponse>("Categoria/ObtenerTodosAsync");
            return result?.data ?? new List<CategoriaModel>();
        }

        public Task<CategoriaModel?> ObtenerPorIdAsync(int id) =>
            _httpService.GetAsync<CategoriaModelResponseSingle>($"Categoria/{id}")
                        .ContinueWith(t => t.Result?.data);

        public Task<bool> CrearAsync(SaveCategoriaModel model) =>
            _httpService.PostAsync<object>("Categoria", model)
                        .ContinueWith(t => t.Result != null);

        public Task<bool> ActualizarAsync(UpdateCategoriaModel model) =>
            _httpService.PutAsync<object>("Categoria", model)
                        .ContinueWith(t => t.Result != null);

        public Task<bool> EliminarAsync(RemoveCategoriaModel model) =>
            _httpService.DeleteAsync("Categoria", model);

        public Task<bool> CrearDesdeModeloAsync(CategoriaModel model) =>
            CrearAsync(_mapper.ConvertToSave(model));

        public Task<bool> ActualizarDesdeModeloAsync(CategoriaModel model) =>
            ActualizarAsync(_mapper.ConvertToUpdate(model));
    }
}




