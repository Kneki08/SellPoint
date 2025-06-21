namespace SellPoint.Persistence.Repositorios
{
    public class CategoriaRepository : GenericRepository<Categoria>, ICategoriaRepository
    {
       public CategoriaRepository(string connectionString) : base(context) { }
    }
}
