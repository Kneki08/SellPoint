namespace SellPoint.Aplication.Interfaces.Repositorios
{
    public interface IProductoRepository : IGenericRepository<Producto>
    {
    Task<OperationResult<List<Producto>>> GetByCategoriaAsync(int categoriaId);
    Task<OperationResult<List<Producto>>> SearchByNombreAsync(string nombre);
    Task<OperationResult> ChangeStatusAsync(int productoId, bool activo);
    }
}
