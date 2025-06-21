
using SellPoint.Domain.Base;
namespace SellPoint.Aplication.Interfaces.Base
{
    public interface IRepository<T> where T : class
    {
        Task<OperationResult> ObtenerPorIdAsync(int id);
        Task<OperationResult> ObtenerTodosAsync();
        Task<OperationResult> AgregarAsync(T entidad);
        Task<OperationResult> ActualizarAsync(T entidad);
        Task<OperationResult> EliminarAsync(int id);

    }
}
