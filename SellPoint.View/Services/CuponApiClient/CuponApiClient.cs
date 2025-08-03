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
            var response = await _httpService.GetAsync<CuponModelResponse>("Cupon/ObtenerTodosAsync");
            return response?.data ?? new List<CuponModel>();
        }

        public async Task<CuponModel?> ObtenerPorIdAsync(int id)
        {
            var response = await _httpService.GetAsync<CuponModelResponseSingle>($"Cupon/{id}");
            return response?.data;
        }

        public async Task<bool> CrearAsync(SaveCuponModel dto)
        {
            var result = await _httpService.PostAsync<object>("Cupon", dto);
            return result != null;
        }

        public async Task<bool> ActualizarAsync(UpdateCuponModel dto)
        {
            var result = await _httpService.PutAsync<object>("Cupon", dto);
            return result != null;
        }

        public async Task<bool> EliminarAsync(RemoveCuponModel dto)
        {
            return await _httpService.DeleteAsync("Cupon", dto);
        }
    }
}
