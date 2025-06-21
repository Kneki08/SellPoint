

using SellPoint.Domain.Base;
using System.Linq.Expressions;

namespace SellPoint.Domain.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<OperationResult> GetAllAsync(Expression<Func<T, bool>> filter);
        Task<T> AddAsync(T entity);
        Task<OperationResult> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> SaveChangesAsync();
    }
}
