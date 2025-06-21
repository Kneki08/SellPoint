using SellPoint.Domainn.Entities.Products;
using SellPoint.Domain.Base;

namespace SellPoint.Aplication.Interfaces.Repositorios
{
    public interface ICuponRepository : IGenericRepository<Cupon>
    {
        Task<OperationResult<Cupon>> ValidateCodigoCuponAsync(string codigo);
    }
}
