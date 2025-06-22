
using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Dtos.Carrito;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;


namespace SellPoint.Persistence.Repositories
{
    public class CarritoRepository : ICarritoRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<CarritoRepository> _logger;

        public CarritoRepository(string connectionString, ILogger<CarritoRepository> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }
        public Task<OperationResult> ActualizarAsync(UpdateCarritoDTO entidad)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> AgregarAsync(SaveCarritoDTO entidad)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> EliminarAsync(RemoveCarritoDTO entidad)
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