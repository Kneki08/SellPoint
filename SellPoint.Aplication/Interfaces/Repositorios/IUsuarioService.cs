using SellPoint.Aplication.Dtos.Categoria;
using SellPoint.Aplication.Interfaces.Base;
using SellPoint.Domain.Base;

namespace SellPoint.Aplication.Interfaces.Repositorios
{
    public interface IUsuarioRepository 
    {
        Task<OperationResult> ObtenerPorIdAsync(int id);
        Task<OperationResult> ObtenerTodosAsync();
        Task<OperationResult> AgregarAsync(SaveCategoriaDTO saveCategoria);
        Task<OperationResult> ActualizarAsync(UpdateCategoriaDTO updateCategoria);
        Task<OperationResult> EliminarAsync(RemoveCategoriaDTO removeCategoria);
    }

}
