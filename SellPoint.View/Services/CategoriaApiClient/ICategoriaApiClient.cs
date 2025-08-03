using SellPoint.View.Models.ModelsCategoria;


namespace SellPoint.View.Services.CategoriaApiClient
{
    public interface ICategoriaApiClient
    {
        Task<IEnumerable<CategoriaModel>> ObtenerTodosAsync();
        Task<CategoriaModel?> ObtenerPorIdAsync(int id);
        Task<bool> CrearAsync(SaveCategoriaModel dto);
        Task<bool> ActualizarAsync(UpdateCategoriaModel dto);
        Task<bool> EliminarAsync(RemoveCategoriaModel dto);
    }
}
