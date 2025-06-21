using SellPoint.Aplication.Interfaces.Base;
using SellPoint.Domain.Base;
using SellPoint.Domainn.Entities.Products;

namespace SellPoint.Aplication.Interfaces.Repositorios
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<OperationResult> ObtenerPorNombreAsync(string nombre);
        Task<OperationResult> ObtenerConProductosAsync(int categoriaId);
    }

}
