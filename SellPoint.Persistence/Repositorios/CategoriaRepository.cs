namespace SellPoint.Persistence.Repositorios
{
    public class CategoriaRepository : ICategoriaRepository
    {
      private readonly string _connectionString;
       public CategoriaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<OperationResult> CreateCategoriaAsync(Catergoria categoria)
        {
            await _connectionString.Categorias.AddAsync(categoria);
            await _connectionString.SaveChangesAsync();
            return OperationResult.Ok("Categoría creada correctamente.");
        }

        public async Task<OperationResult> UpdateCategoriaAsync(Catergoria categoria)
        {
            var existente = await _connectionString.Categorias.FindAsync(categoria.Id);
            if (existente == null)
                return OperationResult.Fail("Categoría no encontrada.");

            existente.Nombre = categoria.Nombre;
            existente.Descripcion = categoria.Descripcion;

            _connectionString.Categorias.Update(existente);
            await _connectionString.SaveChangesAsync();

            return OperationResult.Ok("Categoría actualizada correctamente.");
        }

        public async Task<OperationResult> DeleteCategoriaAsync(int categoriaId)
        {
            var categoria = await _connectionString.Categorias.FindAsync(categoriaId);
            if (categoria == null)
                return OperationResult.Fail("Categoría no encontrada.");

            _connectionString.Categorias.Remove(categoria);
            await _connectionString.SaveChangesAsync();

            return OperationResult.Ok("Categoría eliminada correctamente.");
        }

        public async Task<OperationResult<Categoria>> GetCategoriaByIdAsync(int categoriaId)
        {
            var categoria = await _connectionString.Categorias.FindAsync(categoriaId);
            if (categoria == null)
                return OperationResult<Categoria>.Fail("Categoría no encontrada.");

            return new OperationResult<Categoria>
            {
                Success = true,
                Data = categoria,
                Message = "Categoría obtenida correctamente."
            }
        }

        public async Task<OperationResult<List<Categoria>>> GetAllCategoriaAsync()
        {
            var categorias = await _connectionString.Categorias.ToListAsync();
            return new OperationResult<List<Categoria>>
            {
                Success = true,
                Data = categorias,
                Message = "Categorías obtenidas correctamente."
            }
        }
    }
}
