using SellPoint.Aplication.Interfaces.Base;
using SellPoint.Domain.Base;

namespace SellPoint.Aplication.Interfaces.Repositorios
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<OperationResult> ObtenerPorEmailAsync(string email);
    }

}
