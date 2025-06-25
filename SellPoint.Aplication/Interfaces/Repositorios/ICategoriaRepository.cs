using SellPoint.Aplication.Dtos.Carrito;
using SellPoint.Aplication.Dtos.Categoria;
using SellPoint.Aplication.Interfaces.Base;
using SellPoint.Domain.Base;
using SellPoint.Domainn.Entities.Products;

namespace SellPoint.Aplication.Interfaces.Repositorios
{
    public interface ICategoriaRepository 
    {
        Task<OperationResult> ObtenerPorIdAsync(int id);
        Task<OperationResult> ObtenerTodosAsync();
        Task<OperationResult> AgregarAsync(SaveCategoriaDTO saveCategoria);
        Task<OperationResult> ActualizarAsync(UpdateCategoriaDTO updateCategoria);
        Task<OperationResult> EliminarAsync(RemoveCategoriaDTO removeCategoria);
    }

}
