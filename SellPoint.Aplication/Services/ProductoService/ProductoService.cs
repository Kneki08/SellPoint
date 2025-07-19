using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Dtos.Producto;
using SellPoint.Aplication.Dtos.ProductoDTO;
using SellPoint.Aplication.Interfaces.IService.IProducto;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;

namespace SellPoint.Aplication.Services.ProductoService
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;
        private readonly ILogger<ProductoService> _logger;
        private readonly IConfiguration _configuration;
        //private readonly string _connectionString;
        public ProductoService(IProductoRepository productoRepository, ILogger<ProductoService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _productoRepository = productoRepository;
        }
        public async Task<OperationResult> ActualizarAsync(UpdateProductoDTO updateProducto)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                _logger.LogInformation("Iniciando actualización del producto con ID: {Id}", updateProducto.Id);

               if( updateProducto == null)
                {
                   _logger.LogError("se requiere crear un DTO");
                    return operationResult;
                }
                operationResult = await _productoRepository.ActualizarAsync(updateProducto);

                if (!operationResult.IsSuccess)
                {
                   _logger.LogError("no se pudo actualizar el producto: {Message}", operationResult.Message);
                    return operationResult;
                }

                _logger.LogInformation("Producto actualizado correctamente: {Producto}", updateProducto);
                return operationResult;


            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el producto");
                operationResult.IsSuccess = false;
                operationResult.Message = "Error al actualizar el producto";
            }
            return operationResult;

        }

        public async Task<OperationResult> AgregarAsync(SaveProductoDTO saveProducto)
        {
            try
            {
                _logger.LogInformation("Iniciando adición del producto: {Producto}", saveProducto);

                if (saveProducto is null)
                {
                    _logger.LogError("Se requiere crear un DTO");
                    return OperationResult.Failure("La entidad no puede ser nula.");
                }

                var operationResult = await _productoRepository.AgregarAsync(saveProducto);

                if (!operationResult.IsSuccess)
                {
                    _logger.LogError("No se pudo agregar el producto: {Message}", operationResult.Message);
                    return operationResult;
                }

                _logger.LogInformation("Producto agregado correctamente: {Producto}", saveProducto);
                return operationResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar el producto");
                return OperationResult.Failure("Error al agregar el producto");

            }
        }

        public async Task<OperationResult> EliminarAsync(RemoveProductoDTO removeProducto)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
              _logger.LogInformation("Iniciando eliminación del producto con ID: {Id}", removeProducto.Id);
                if (removeProducto is null)
                {
                    _logger.LogError("Se requiere crear un DTO");
                    return operationResult;
                }
                operationResult = await _productoRepository.EliminarAsync(removeProducto);
                if (!operationResult.IsSuccess)
                {
                    _logger.LogError("No se pudo eliminar el producto: {Message}", operationResult.Message);
                    return operationResult;
                }
                _logger.LogInformation("Producto eliminado correctamente: {Producto}", removeProducto);
                return operationResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el producto");
                operationResult.IsSuccess = false;
                operationResult.Message = "Error al eliminar el producto";
            }
            return operationResult;
        }

        public async Task<OperationResult> ObtenerPorIdAsync(int obtenerProducto)
        {
           OperationResult operationResult = new OperationResult();
            try
            {
                if (obtenerProducto <= 0)
                {
                    _logger.LogError("ID de producto inválido: {Id}", obtenerProducto);
                    return operationResult;
                }
                operationResult = await _productoRepository.ObtenerPorIdAsync(obtenerProducto);
                if (!operationResult.IsSuccess)
                {
                    _logger.LogError("No se pudo obtener el producto: {Message}", operationResult.Message);
                    return operationResult;
                }
               
                return operationResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el producto");
                operationResult.IsSuccess = false;
                operationResult.Message = "Error al obtener el producto";
            }
            return operationResult;
        }

        public async Task<OperationResult> ObtenerTodosAsync()
        {
            OperationResult operation = new OperationResult();
            try
            {
              
                operation = await _productoRepository.ObtenerTodosAsync();

                if (!operation.IsSuccess)
                {
                    _logger.LogError("No se pudieron obtener los productos: {Message}", operation.Message);
                    return operation;
                }
                _logger.LogInformation("Productos obtenidos correctamente");
                return operation;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los productos");
                operation.IsSuccess = false;
                operation.Message = "Error al obtener todos los productos";
               
            }
            return operation;
        }
    }
}
