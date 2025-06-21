namespace SellPoint.Persistence.Repositorios
{
    public class ProductoRepository : IProductoRepository
    {
    private readonly string _connectionString;

        public ProductoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<OperationResult> CreateProductoAsync(Producto producto)
        {
            await _connectionString.Productos.AddAsync(producto);
            await _connectionString.SaveChangesAsync();
            return OperationResult.Ok("Producto creado correctamente.");
        }

        public async Task<OperationResult> UpdateProductoAsync(Producto producto)
        {
            var existente = await _connectionString.Productos.FindAsync(producto.Id);
            if (existente == null)
                return OperationResult.Fail("Producto no encontrado.");

            existente.Nombre = producto.Nombre;
            existente.Descripcion = producto.Descripcion;
            existente.Precio = producto.Precio;
            existente.Stock = producto.Stock;
            existente.CategoriaId = producto.CategoriaId;
            existente.ImagenUrl = producto.ImagenUrl;
            existente.Activo = producto.Activo;

            _connectionString.Productos.Update(existente);
            await _connectionString.SaveChangesAsync();

            return OperationResult.Ok("Producto actualizado correctamente.");
        }

        public async Task<OperationResult> DeleteProductoAsync(int productoId)
        {
            var producto = await _connectionString.Productos.FindAsync(productoId);
            if (producto == null)
                return OperationResult.Fail("Producto no encontrado.");

            _connectionString.Productos.Remove(producto);
            await _connectionString.SaveChangesAsync();

            return OperationResult.Ok("Producto eliminado correctamente.");
        }

        public async Task<OperationResult<Producto>> GetProductoByIdAsync(int productoId)
        {
            var producto = await _connectionString.Productos.FindAsync(productoId);
            if (producto == null)
                return OperationResult<Producto>.Fail("Producto no encontrado.");

            return new OperationResult<Producto>
            {
                Success = true,
                Data = producto,
                Message = "Producto obtenido correctamente."
            }
        }

        public async Task<OperationResult<List<Producto>>> GetAllProductosAsync()
        {
            var productos = await _connectionString.Productos.ToListAsync();
            return new OperationResult<List<Producto>>
            {
                Success = true,
                Data = productos,
                Message = "Lista de productos obtenida correctamente."
            }
        }

        public async Task<OperationResult<List<Producto>>> GetByCategoriaAsync(int categoriaId)
        {
            var productos = await _connectionString.Productos
                .Where(p => p.CategoriaId == categoriaId)
                .ToListAsync();

            return new OperationResult<List<Producto>>
            {
                Success = true,
                Data = productos,
                Message = "Productos por categoría obtenidos correctamente."
            }
        }

        public async Task<OperationResult<List<Producto>>> SearchByNombreAsync(string nombre)
        {
            var productos = await _connectionString.Productos
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
            var producto = await _connectionString.Productos.FindAsync(productoId);
            if (producto == null)
                return OperationResult.Fail("Producto no encontrado.");

            producto.Activo = activo;
            _connectionString.Productos.Update(producto);
            await _connectionString.SaveChangesAsync();

            return OperationResult.Ok("Estado del producto actualizado correctamente.");
        }
    }
}
