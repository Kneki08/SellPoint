namespace SellPoint.Aplication.Interfaces.Repositorios
{
    public interface ICategoriaRepository
    {
        Task<OperationResult> CreateCategoriaAsync(Catergoria categoria);
        Task<OperationResult> UpdateCategoriaAsync(Catergoria categoria);
        Task<OperationResult> DeleteCategoriaAsync(int categoriaId);
        Task<OperationResult<Categoria>> GetCategoriaByIdAsync(int categoriaId);
        Task<OperationResult<List<Categoria>>> GetAllCategoriaAsync();
    }
}
