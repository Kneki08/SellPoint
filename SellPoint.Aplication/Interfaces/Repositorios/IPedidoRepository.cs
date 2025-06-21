namespace SellPoint.Aplication.Interfaces.Repositorios
{
    public interface IPedidoRepository
    {
        Task<OperationResult> CreatePedidoAsync(Pedido pedido);
        Task<OperationResult> UpdatePedidoAsync(Pedido pedido);
        Task<OperationResult> DeletePedidoAsync(int pedidoId);
        Task<OperationResult<Pedido>> GetPedidoByIdAsync(int pedidoId);
        Task<OperationResult<List<Pedido>>> GetAllPedidosAsync();
        Task<OperationResult<List<Pedido>>> GetPedidosByClienteIdAsync(int clienteId);
        Task<OperationResult> ChangeStatusPedidoAsync(int pedidoId, string nuevoEstado);
    }
}
