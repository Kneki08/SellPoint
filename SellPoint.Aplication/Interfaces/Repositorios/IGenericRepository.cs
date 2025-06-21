using SellPoint.Domain.Base;
namespace SellPoint.Aplication.Interfaces.Repositorios
{
    public interface IGenericRepository<T> where T : class
    {
        Task<OperationResult> GetByIdAsync(int id);
        Task<OperationResult> GetAllAsync();
        Task<OperationResult> CreateAsync(T entity);
        Task<OperationResult> UpdateAsync(T entity);
        Task<OperationResult> DeleteAsync(int id);

    }
}
