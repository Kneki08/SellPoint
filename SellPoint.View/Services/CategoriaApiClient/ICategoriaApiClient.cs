using SellPoint.Aplication.Dtos.Categoria;


namespace SellPoint.View.Services.CategoriaApiClient
{
    public interface ICategoriaApiClient
    {
        Task<IEnumerable<CategoriaDTO>> ObtenerTodosAsync();
        Task<CategoriaDTO?> ObtenerPorIdAsync(int id);
        Task<bool> CrearAsync(SaveCategoriaDTO dto);
        Task<bool> ActualizarAsync(UpdateCategoriaDTO dto);
        Task<bool> EliminarAsync(RemoveCategoriaDTO dto);
    }
}
