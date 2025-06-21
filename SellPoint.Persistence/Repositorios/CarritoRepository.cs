


using SellPoint.Domain.Base;

namespace SellPoint.Persistence.Repositorios
{
    public class CarritoRepository : ICarritoRepository
    {
    private readonly string _connectionString;

        public CarritoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<OperationResult> AddProductoAsync(int productoId, int cantidad)
        {
            if (cantidad <= 0)
                return OperationResult.Fail("La cantidad debe ser mayor que cero.");

            var carrito = await _context.Carritos
                .FirstOrDefaultAsync(c => c.ProductoId == productoId);

            if (carrito != null)
            {
                carrito.Cantidad += cantidad;
                carrito.FechaActualizacion = DateTime.UtcNow;
            }
            else
            {
                carrito = new Carrito
                {
                    ProductoId = productoId,
                    Cantidad = cantidad,
                    FechaAgregado = DateTime.UtcNow,
                    FechaActualizacion = DateTime.UtcNow,
                    UsuarioId = 1 
                }
                await _connectionString.Carritos.AddAsync(carrito);
            }

            await _connectionString.SaveChangesAsync();
            return OperationResult.Ok("Producto agregado al carrito.");
        }

        public async Task<OperationResult> DeleteProductoAsync(int productoId)
        {
            var carrito = await _connectionString.Carritos.FirstOrDefaultAsync(c => c.ProductoId == productoId);
            if (carrito == null)
                return OperationResult.Fail("Producto no encontrado en el carrito.");

            _connectionString.Carritos.Remove(carrito);
            await _connectionString.SaveChangesAsync();

            return OperationResult.Ok("Producto eliminado del carrito.");
        }

        public async Task<OperationResult> CleanCarritoAsync()
        {
            var items = await _connectionString.Carritos.ToListAsync();
            _connectionString.Carritos.RemoveRange(items);
            await _connectionString.SaveChangesAsync();

            return OperationResult.Ok("Carrito vaciado correctamente.");
        }

        public async Task<OperationResult<List<Carrito>>> GetItemsAsync()
        {
            var items = await _connectionString.Carritos.ToListAsync();
            return new OperationResult<List<Carrito>>
            {
                Success = true,
                Data = items,
                Message = "Productos del carrito obtenidos correctamente."
            }
        }

        public async Task<OperationResult<decimal>> GetTotalAsync()
        {
            var items = await _connectionString.Carritos.ToListAsync();

          
            decimal total = 0;
            foreach (var item in items)
            {
               
                decimal precio = 100; 
                total += item.Cantidad * precio;
            }

            return new OperationResult<decimal>
            {
                Success = true,
                Data = total,
                Message = "Total del carrito calculado correctamente."
            }
        }

        public async Task<OperationResult> UpdateCantidadAsync(int productoId, int nuevaCantidad)
        {
            if (nuevaCantidad <= 0)
                return OperationResult.Fail("La cantidad debe ser mayor que cero.");

            var carrito = await _connectionString.Carritos.FirstOrDefaultAsync(c => c.ProductoId == productoId);
            if (carrito == null)
                return OperationResult.Fail("Producto no encontrado en el carrito.");

            carrito.Cantidad = nuevaCantidad;
            carrito.FechaActualizacion = DateTime.UtcNow;

            await _connectionString.SaveChangesAsync();
            return OperationResult.Ok("Cantidad actualizada correctamente.");
        }
    }
}
