namespace SellPoint.Aplication.Interfaces.Repositorios
{
    public interface IDetallePedidoRepository : IGenericRepository<DetallePedido>
    {
        Task<OperationResult<List<DetallePedido>>> GetDetallesByPedidoIdAsync(int pedidoId);
    }
}
