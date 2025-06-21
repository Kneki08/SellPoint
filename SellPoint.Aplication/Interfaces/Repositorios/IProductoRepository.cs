using SellPoint.Aplication.Interfaces.Base;
using SellPoint.Domain.Base;
using SellPoint.Domainn.Entities.Products;

namespace SellPoint.Aplication.Interfaces.Repositorios
{
    public interface IProductoRepository : IRepository<Producto>
    {
        Task<OperationResult> ObtenerPorCategoriaAsync(int categoriaId);
        Task<OperationResult> BuscarPorNombreAsync(string nombre);
    }


}
