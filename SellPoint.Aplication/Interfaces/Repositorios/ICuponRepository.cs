namespace SellPoint.Aplication.Interfaces.Repositorios
{
    public interface ICuponRepository : IGenericRepository<Cupon>
    {
        Task<OperationResult<Cupon>> ValidateCodigoCuponAsync(string codigo);
    }
}
