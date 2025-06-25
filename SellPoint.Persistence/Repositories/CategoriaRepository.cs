using SellPoint.Aplication.Dtos.Categoria;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;

namespace SellPoint.Persistence.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        public Task<OperationResult> ActualizarAsync(UpdateCategoriaDTO updateCategoria)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> AgregarAsync(SaveCategoriaDTO saveCategoria)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> EliminarAsync(RemoveCategoriaDTO removeCategoria)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> ObtenerPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<OperationResult> ObtenerTodosAsync()
        {
            throw new NotImplementedException();
        }
    }
}
