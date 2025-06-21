namespace SellPoint.Persistence.Repositorios
{
    public class DetallePedidoRepository : IDetallePedidoRepository
    {
      private readonly string _connectionString;

        public DetallePedidoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<OperationResult> CreateDetalleAsync(DetallePedido detalle)
        {
            await _connectionString.DetallesPedido.AddAsync(detalle);
            await _connectionString.SaveChangesAsync();
            return OperationResult.Ok("Detalle del pedido creado correctamente.");
        }

        public async Task<OperationResult> UpdateDetalleAsync(DetallePedido detalle)
        {
            var existente = await _connectionString.DetallesPedido.FindAsync(detalle.Id);
            if (existente == null)
                return OperationResult.Fail("Detalle no encontrado.");

            existente.Pedidoid = detalle.Pedidoid;
            existente.ProductoId = detalle.ProductoId;
            existente.Cantidad = detalle.Cantidad;
            existente.PrecioUnitario = detalle.PrecioUnitario;
            existente.Fechaagregado = detalle.Fechaagregado;

            _connectionString.DetallesPedido.Update(existente);
            await _context.SaveChangesAsync();

            return OperationResult.Ok("Detalle actualizado correctamente.");
        }

        public async Task<OperationResult> DeleteDetalleAsync(int detalleId)
        {
            var detalle = await _connectionString.DetallesPedido.FindAsync(detalleId);
            if (detalle == null)
                return OperationResult.Fail("Detalle no encontrado.");

            _connectionString.DetallesPedido.Remove(detalle);
            await _context.SaveChangesAsync();

            return OperationResult.Ok("Detalle eliminado correctamente.");
        }

        public async Task<OperationResult<DetallePedido>> GetDetalleByIdAsync(int detalleId)
        {
            var detalle = await _connectionString.DetallesPedido.FindAsync(detalleId);
            if (detalle == null)
                return OperationResult<DetallePedido>.Fail("Detalle no encontrado.");

            return new OperationResult<DetallePedido>
            {
                Success = true,
                Data = detalle,
                Message = "Detalle obtenido correctamente."
            }
        }

        public async Task<OperationResult<List<DetallePedido>>> GetDetallesByPedidoIdAsync(int pedidoId)
        {
            var detalles = await _connectionString.DetallesPedido
                .Where(d => d.Pedidoid == pedidoId)
                .ToListAsync();

            return new OperationResult<List<DetallePedido>>
            {
                Success = true,
                Data = detalles,
                Message = "Detalles del pedido obtenidos correctamente."
            }
        }
    }
}
