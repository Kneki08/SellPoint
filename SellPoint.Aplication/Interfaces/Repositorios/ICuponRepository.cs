
using SellPoint.Domain.Base;
using SellPoint.Domainn.Entities.Products;
using SellPoint.Aplication.Interfaces.Base;

namespace SellPoint.Aplication.Interfaces.Repositorios
{
    public interface ICuponRepository : IRepository<Cupon>
    {
        Task<OperationResult> ObtenerPorCodigoAsync(string codigo);
        Task<OperationResult> ValidarCuponAsync(string codigo);
        Task<OperationResult> MarcarComoUsadoAsync(string codigo, int usuarioId);
    }

}
