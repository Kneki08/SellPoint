
namespace SellPoint.Aplication.Interfaces.Repositorios
{
    public interface ICarritoRepository
    {
        Task<OperationResult> AddProductoAsync(int productoId, int cantidad);
        Task<OperationResult> DeleteProductoAsync(int productoId);
        Task<OperationResult> CleanCarritoAsync();
        Task<OperationResult<List<CarritoItem>>> GetItemsAsync();
        Task<OperationResult<decimal>> GetTotalAsync();
        Task<OperationResult> UpdateCantidadAsync(int productoId, int nuevaCantidad);
    }
}
