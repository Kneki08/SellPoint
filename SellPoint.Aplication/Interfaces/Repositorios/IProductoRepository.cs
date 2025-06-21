namespace SellPoint.Aplication.Interfaces.Repositorios
{
    public interface IProductoRepository
    {
        Task<OperationResult> CreateProductoAsync(Producto producto);
        Task<OperationResult> UpdateProductoAsync(Producto producto);
        Task<OperationResult> DeleteProductoAsync(int productoId);
        Task<OperationResult<Producto>> GetProductoByIdAsync(int productoId);
        Task<OperationResult<List<Producto>>> GetAllProductosAsync();
        Task<OperationResult<List<Producto>>> GetByCategoriaAsync(int categoriaId);
        Task<OperationResult<List<Producto>>> SearchByNombreAsync(string nombre);
        Task<OperationResult> ChangeStatusAsync(int productoId, bool activo);
    }
}
