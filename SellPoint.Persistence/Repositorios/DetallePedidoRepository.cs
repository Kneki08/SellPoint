namespace SellPoint.Persistence.Repositorios
{
    public class DetallePedidoRepository : GenericRepository<DetallePedido>, IDetallePedidoRepository
    {

        public DetallePedidoRepository(string connectionString) base(connectionString) { }
       
        public async Task<OperationResult<List<DetallePedido>>> GetDetallesByPedidoIdAsync(int pedidoId)
        {
            var detalles = await _dbSet
                .Where(d => d.Pedidoid == pedidoId)
                .ToListAsync();

            return new OperationResult<List<DetallePedido>>
            {
                Success = true,
                Data = detalles,
                Message = "Detalles del pedido obtenidos."
            }
        }
    }
}
