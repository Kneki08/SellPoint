using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Dtos.Categoria;
using SellPoint.Aplication.Dtos.Producto;
using SellPoint.Aplication.Dtos.ProductoDTO;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;
using SellPoint.Domainn.Entities.Products;

namespace SellPoint.Persistence.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<ProductoRepository> _logger;

        public ProductoRepository(string connectionString,ILogger<ProductoRepository> logger)
        {
            _connectionString = connectionString;
        }

        public Task<OperationResult> ActualizarAsync(UpdateProductoDTO entidad)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> AgregarAsync(SaveProductoDTO entidad)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> EliminarAsync(RemoveProductoDTO entidad)
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
