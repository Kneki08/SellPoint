namespace SellPoint.Aplication.Interfaces.Repositorios
{
    public interface ICategoriaRepository
    {
        Task<OperationResult> CreateCategoriaAsync(Categoria categoria);
        Task<OperationResult> UpdateCategoriaAsync(Categoria categoria);
        Task<OperationResult> DeleteCategoriaAsync(int categoriaId);
        Task<OperationResult<Categoria>> GetCategoriaByIdAsync(int categoriaId);
        Task<OperationResult<List<Categoria>>> GetAllCategoriaAsync();
    }
}
