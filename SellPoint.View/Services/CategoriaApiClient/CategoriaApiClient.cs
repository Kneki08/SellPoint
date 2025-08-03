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
            var response = await _httpService.GetAsync<CategoriaModelResponse>("Categoria/ObtenerTodosAsync");
            return response?.data ?? new List<CategoriaModel>();
        }

        public async Task<CategoriaModel?> ObtenerPorIdAsync(int id)
        {
            var response = await _httpService.GetAsync<CategoriaModelResponseSingle>($"Categoria/{id}");
            return response?.data;
        }

        public async Task<bool> CrearAsync(SaveCategoriaModel dto)
        {
            var result = await _httpService.PostAsync<object>("Categoria", dto);
            return result != null;
        }

        public async Task<bool> CrearDesdeModeloAsync(CategoriaModel model)
        {
            var dto = _mapper.ConvertToSave(model);
            return await CrearAsync(dto);
        }

        public async Task<bool> ActualizarAsync(UpdateCategoriaModel dto)
        {
            var result = await _httpService.PutAsync<object>("Categoria", dto);
            return result != null;
        }

        public async Task<bool> ActualizarDesdeModeloAsync(CategoriaModel model)
        {
            var dto = _mapper.ConvertToUpdate(model);
            return await ActualizarAsync(dto);
        }

        public async Task<bool> EliminarAsync(RemoveCategoriaModel dto)
        {
            return await _httpService.DeleteAsync("Categoria", dto);
        }
    }
}


