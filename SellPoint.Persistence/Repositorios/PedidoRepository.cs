namespace SellPoint.Persistence.Repositorios
{
    public class PedidoRepository : GenericRepository<Pedido>, IPedidoRepository
    {

        public PedidoRepository(string connectionString): base(context) { }

        public async Task<OperationResult<List<Pedido>>> GetPedidosByClienteIdAsync(int clienteId)
        {
            var pedidos = await _dbSet
                .Where(p => p.UsuarioId == clienteId)
                .ToListAsync();

            return new OperationResult<List<Pedido>>
            {
                Success = true,
                Data = pedidos,
                Message = "Pedidos del cliente obtenidos."
            }
        }

        public async Task<OperationResult> ChangeStatusPedidoAsync(int pedidoId, string nuevoEstado)
        {
            var pedido = await _dbSet.FindAsync(pedidoId);
            if (pedido == null)
                return OperationResult.Fail("Pedido no encontrado.");

            if (Enum.TryParse(nuevoEstado, out EstadoPedido estado))
            {
                pedido.Estado = estado;
                _dbSet.Update(pedido);
                await _connectionString.SaveChangesAsync();
                return OperationResult.Ok("Estado actualizado correctamente.");
            }

            return OperationResult.Fail("Estado inválido.");
        }
    }
}
