using SellPoint.Aplication.Dtos.Categoria;
using SellPoint.Aplication.Dtos.Cupon;
using SellPoint.View.Models.ModelsCupon;


namespace SellPoint.View.Services.CuponApiClient
{
    public interface ICuponApiClient
    {
        Task<IEnumerable<CuponModel>> ObtenerTodosAsync();
        Task<CuponModel?> ObtenerPorIdAsync(int id);
        Task<bool> CrearAsync(SaveCuponModel dto);
        Task<bool> ActualizarAsync(UpdateCuponModel dto);
        Task<bool> EliminarAsync(RemoveCuponModel dto);
    }
}
