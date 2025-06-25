using SellPoint.Aplication.Dtos.Cupon;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;


namespace SellPoint.Persistence.Repositories
{
    public class CuponRepository : ICuponRepository
    {
        public Task<OperationResult> ActualizarAsync(UpdateCuponDTO entidad)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> AgregarAsync(SaveCuponDTO entidad)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> EliminarAsync(RemoveCuponDTIO entidad)
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
