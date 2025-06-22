using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;
using SellPoint.Domainn.Entities.Products;


namespace SellPoint.Persistence.Repositories
{
    public class CuponRepository : ICuponRepository
    {
        public Task<OperationResult> ActualizarAsync(Cupon entidad)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> AgregarAsync(Cupon entidad)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> EliminarAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> MarcarComoUsadoAsync(string codigo, int usuarioId)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> ObtenerPorCodigoAsync(string codigo)
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

        public Task<OperationResult> ValidarCuponAsync(string codigo)
        {
            throw new NotImplementedException();
        }
    }
}
