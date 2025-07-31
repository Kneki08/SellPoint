using SellPoint.Aplication.Dtos.Categoria;
using SellPoint.Aplication.Dtos.Cupon;


namespace SellPoint.View.Services.CuponApiClient
{
    public interface ICuponApiClient
    {
        Task<IEnumerable<CuponDTO>> ObtenerTodosAsync();
        Task<CuponDTO?> ObtenerPorIdAsync(int id);
        Task<bool> CrearAsync(SaveCuponDTO dto);
        Task<bool> ActualizarAsync(UpdateCuponDTO dto);
        Task<bool> EliminarAsync(RemoveCuponDTIO dto);
    }
}
