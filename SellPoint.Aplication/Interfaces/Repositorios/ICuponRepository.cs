namespace SellPoint.Aplication.Interfaces.Repositorios
{
    public interface ICuponRepository
    {
        Task<OperationResult> CreateCuponAsync(Cupon cupon);
        Task<OperationResult> UpdateCuponAsync(Cupon cupon);
        Task<OperationResult> DeleteCuponAsync(int cuponId);
        Task<OperationResult<Cupon>> GetCuponByIdAsync(int cuponId);
        Task<OperationResult<List<Cupon>>> GetAllCuponAsync();
        Task<OperationResult<Cupon>> ValidateCodigoCuponAsync(string codigo);
    }
}
