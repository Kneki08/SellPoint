namespace SellPoint.Aplication.Interfaces.Repositorios
{
    public interface IGenericRepository<T> where T : class
    {
        Task<OperationResult> CreateAsync(T entity);
        Task<OperationResult> UpdateAsync(T entity);
        Task<OperationResult> DeleteAsync(int id);
        Task<OperationResult<T>> GetByIdAsync(int id);
        Task<OperationResult<List<T>>> GetAllAsync();
    }
}
