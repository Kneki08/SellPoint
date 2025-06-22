using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;
using SellPoint.Domainn.Entities.Products;

namespace SellPoint.Persistence.Repositories
{
    internal class ProductoRepository : IProductoRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<ProductoRepository> _logger;

        public ProductoRepository(string connectionString,ILogger<ProductoRepository> logger)
        {
            _connectionString = connectionString;
        }

        public Task<OperationResult> AgregarAsync(Producto entidad)
        {
            try
            {

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar producto");
                return Task.FromResult(new OperationResult(false, "Error al agregar producto", ex));
            }
        }

        public Task<OperationResult> ActualizarAsync(Producto entidad)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> BuscarPorNombreAsync(string nombre)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> EliminarAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> ObtenerPorCategoriaAsync(int categoriaId)
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
