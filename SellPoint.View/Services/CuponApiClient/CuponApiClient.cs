using SellPoint.View.Mappers.Cupon;
using SellPoint.View.Models.ModelsCupon;

namespace SellPoint.View.Services.CuponApiClient
{
    public class CuponApiClient : ICuponApiClient
    {
        private readonly HttpServiceBase _httpService;
        private readonly ICuponMapper _mapper;

        public CuponApiClient(HttpServiceBase httpService, ICuponMapper mapper)
        {
            _httpService = httpService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CuponModel>> ObtenerTodosAsync()
        {
            var result = await _httpService.GetAsync<CuponModelResponse>("Cupon/ObtenerTodosAsync");
            return result?.data ?? new List<CuponModel>();
        }

        public Task<CuponModel?> ObtenerPorIdAsync(int id) =>
            _httpService.GetAsync<CuponModelResponseSingle>($"Cupon/{id}")
                        .ContinueWith(t => t.Result?.data);

        public Task<bool> CrearAsync(SaveCuponModel model) =>
            _httpService.PostAsync<object>("Cupon", model)
                        .ContinueWith(t => t.Result != null);

        public Task<bool> ActualizarAsync(UpdateCuponModel model) =>
            _httpService.PutAsync<object>("Cupon", model)
                        .ContinueWith(t => t.Result != null);

        public Task<bool> EliminarAsync(RemoveCuponModel model) =>
            _httpService.DeleteAsync("Cupon", model);
    }
}
