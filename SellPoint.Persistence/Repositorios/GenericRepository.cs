using SellPoint.Aplication.Interfaces.Base;
using SellPoint.Domain.Base;

namespace SellPoint.Persistence.Repositorios
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly DbSet<T> _dbSet;
        private readonly string _connectionString;

        public GenericRepository(string connectionString)
        {
            _connectionString = connectionString;
           _dbSet = _context.Set<T>();
        }
    
        public async Task<OperationResult> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _connectionString.SaveChangesAsync();
            return OperationResult.Ok($"{typeof(T).Name} creado correctamente.");
        }

        public async Task<OperationResult> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _connectionString.SaveChangesAsync();
            return OperationResult.Ok($"{typeof(T).Name} actualizado correctamente.");
        }

        public async Task<OperationResult> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                return OperationResult.Fail($"{typeof(T).Name} no encontrado.");

            _dbSet.Remove(entity);
            await _connectionString.SaveChangesAsync();
            return OperationResult.Ok($"{typeof(T).Name} eliminado correctamente.");
        }

        public async Task<OperationResult<T>> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                return OperationResult<T>.Fail($"{typeof(T).Name} no encontrado.");

            return new OperationResult<T>
            {
                Success = true,
                Data = entity,
                Message = $"{typeof(T).Name} obtenido correctamente."
            }
        }

        public async Task<OperationResult<List<T>>> GetAllAsync()
        {
            var list = await _dbSet.ToListAsync();
            return new OperationResult<List<T>>
            {
                Success = true,
                Data = list,
                Message = $"Lista de {typeof(T).Name} obtenida correctamente."
            }
        }
    }
}
