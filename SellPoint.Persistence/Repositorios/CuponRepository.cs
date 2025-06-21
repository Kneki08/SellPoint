namespace SellPoint.Persistence.Repositorios
{
    public class CuponRepository : GenericRepository<Cupon>, ICuponRepository
    {
       public CuponRepository(string connectionString) : base(context) { }
        
       public async Task<OperationResult<Cupon>> ValidateCodigoCuponAsync(string codigo)
     {
        var cupon = await _dbSet.FirstOrDefaultAsync(c => c.Codigo == codigo && c.Activo);
        if (cupon == null)
            return OperationResult<Cupon>.Fail("Cupón inválido o no activo.");

        return new OperationResult<Cupon>
        {
            Success = true,
            Data = cupon,
            Message = "Cupón válido."
        }
      }
   }
}
