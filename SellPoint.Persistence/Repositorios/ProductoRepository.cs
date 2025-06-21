namespace SellPoint.Persistence.Repositorios
{
    public class ProductoRepository : GenericRepository<Producto>, IProductoRepository
    {
        public ProductoRepository(string connectionString) : base(context) { }
       
    public async Task<OperationResult<List<Producto>>> GetByCategoriaAsync(int categoriaId)
    {
        var productos = await _dbSet
            .Where(p => p.CategoriaId == categoriaId)
            .ToListAsync();

        return new OperationResult<List<Producto>>
        {
            Success = true,
            Data = productos,
            Message = "Productos filtrados por categoría."
        }
    }

    public async Task<OperationResult<List<Producto>>> SearchByNombreAsync(string nombre)
    {
        var productos = await _dbSet
            .Where(p => p.Nombre.Contains(nombre))
            .ToListAsync();

        return new OperationResult<List<Producto>>
        {
            Success = true,
            Data = productos,
            Message = "Productos encontrados por nombre."
        }
    }

    public async Task<OperationResult> ChangeStatusAsync(int productoId, bool activo)
    {
        var producto = await _dbSet.FindAsync(productoId);
        if (producto == null)
            return OperationResult.Fail("Producto no encontrado.");

        producto.Activo = activo;
        _dbSet.Update(producto);
        await _connectionString .SaveChangesAsync();

        return OperationResult.Ok("Estado actualizado.");
    }
 }
 }
