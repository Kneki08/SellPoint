namespace SellPoint.Aplication.Interfaces.Repositorios
{
    public interface IDetallePedidoRepository
    {
        Task<OperationResult> CreateDetalleAsync(DetallePedido detalle);
        Task<OperationResult> UpdateDetalleAsync(DetallePedido detalle);
        Task<OperationResult> DeleteDetalleAsync(int detalleId);
        Task<OperationResult<DetallePedido>> GetDetalleByIdAsync(int detalleId);
        Task<OperationResult<List<DetallePedido>>> GetDetallesByPedidoIdAsync(int pedidoId);
    }
}
