namespace SellPoint.Persistence.Repositorios
{
    public class PedidoRepository : IPedidoRepository
    {
     private readonly string _connectionString;

        public PedidoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

         public async Task<OperationResult> CreatePedidoAsync(Pedido pedido)
        {
            pedido.FechaPedido = DateTime.UtcNow;
            pedido.FechaActualizacion = DateTime.UtcNow;

            await _connectionString.Pedidos.AddAsync(pedido);
            await _connectionString.SaveChangesAsync();
            return OperationResult.Ok("Pedido creado correctamente.");
        }

        public async Task<OperationResult> UpdatePedidoAsync(Pedido pedido)
        {
            var existente = await _connectionString.Pedidos.FindAsync(pedido.Id);
            if (existente == null)
                return OperationResult.Fail("Pedido no encontrado.");

            existente.UsuarioId = pedido.UsuarioId;
            existente.NumeroPedido = pedido.NumeroPedido;
            existente.Subtotal = pedido.Subtotal;
            existente.Descuento = pedido.Descuento;
            existente.CostoEnvio = pedido.CostoEnvio;
            existente.Estado = pedido.Estado;
            existente.MetodoPago = pedido.MetodoPago;
            existente.ReferenciaPago = pedido.ReferenciaPago;
            existente.CuponId = pedido.CuponId;
            existente.DireccionEnvioId = pedido.DireccionEnvioId;
            existente.Notas = pedido.Notas;
            existente.FechaActualizacion = DateTime.UtcNow;

            _connectionString.Pedidos.Update(existente);
            await _connectionString.SaveChangesAsync();

            return OperationResult.Ok("Pedido actualizado correctamente.");
        }

        public async Task<OperationResult> DeletePedidoAsync(int pedidoId)
        {
            var pedido = await _connectionString.Pedidos.FindAsync(pedidoId);
            if (pedido == null)
                return OperationResult.Fail("Pedido no encontrado.");

            _connectionString.Pedidos.Remove(pedido);
            await _connectionString.SaveChangesAsync();

            return OperationResult.Ok("Pedido eliminado correctamente.");
        }

        public async Task<OperationResult<Pedido>> GetPedidoByIdAsync(int pedidoId)
        {
            var pedido = await _connectionString.Pedidos.FindAsync(pedidoId);
            if (pedido == null)
                return OperationResult<Pedido>.Fail("Pedido no encontrado.");

            return new OperationResult<Pedido>
            {
                Success = true,
                Data = pedido,
                Message = "Pedido obtenido correctamente."
            }
        }

        public async Task<OperationResult<List<Pedido>>> GetAllPedidosAsync()
        {
            var pedidos = await _connectionString.Pedidos.ToListAsync();

            return new OperationResult<List<Pedido>>
            {
                Success = true,
                Data = pedidos,
                Message = "Lista de pedidos obtenida correctamente."
            }
        }

        public async Task<OperationResult<List<Pedido>>> GetPedidosByClienteIdAsync(int clienteId)
        {
            var pedidos = await _connectionString.Pedidos
                .Where(p => p.UsuarioId == clienteId)
                .ToListAsync();

            return new OperationResult<List<Pedido>>
            {
                Success = true,
                Data = pedidos,
                Message = "Pedidos del cliente obtenidos correctamente."
            }
        }

        public async Task<OperationResult> ChangeStatusPedidoAsync(int pedidoId, string nuevoEstado)
        {
            var pedido = await _connectionString.Pedidos.FindAsync(pedidoId);
            if (pedido == null)
                return OperationResult.Fail("Pedido no encontrado.");

            if (!Enum.TryParse<EstadoPedido>(nuevoEstado, out var estado))
                return OperationResult.Fail("Estado de pedido inválido.");

            pedido.Estado = estado;
            pedido.FechaActualizacion = DateTime.UtcNow;

            _connectionString.Pedidos.Update(pedido);
            await _connectionString.SaveChangesAsync();

            return OperationResult.Ok("Estado del pedido actualizado correctamente.");
        }
    }
}
