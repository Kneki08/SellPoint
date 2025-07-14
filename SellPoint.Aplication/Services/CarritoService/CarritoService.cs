using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Dtos.Carrito;
using SellPoint.Aplication.Interfaces.IService.ICarrito;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;

namespace SellPoint.Aplication.Services.CarritoService
{
    public sealed class CarritoService : ICarritoService
    {
        private readonly ICarritoRepository _CarritoRepository;
        private readonly ILogger<CarritoService> _logger;
        private readonly IConfiguration _configuration;
        //private readonly string _connectionString;

        public CarritoService(ICarritoRepository CarritoRepository,ILogger<CarritoService> logger, IConfiguration Configuration) 
        {
            _CarritoRepository = CarritoRepository;
            _logger = logger;
            _configuration = Configuration;
        }
        public async Task<OperationResult> ActualizarAsync(UpdateCarritoDTO updateCarrito)
        {
            OperationResult operation = new OperationResult();

            try
            {
                _logger.LogInformation("Actualizando el carrito", updateCarrito);

                if(updateCarrito is null)
                {
                    _logger.LogError("Se requiere crear un DTO");   
                    return operation;
                }
                operation = await _CarritoRepository.ActualizarAsync(updateCarrito);
               
                if(!operation.IsSuccess)
                {
                    _logger.LogError("No se pudo actualizar el carrito: {Message}", operation.Message);
                    return operation;
                }

                _logger.LogInformation("Carrito actualizado correctamente para UsuarioId: {UsuarioId}, ProductoId: {ProductoId}, NuevaCantidad: {NuevaCantidad}",
                    updateCarrito.UsuarioId, updateCarrito.ProductoId, updateCarrito.NuevaCantidad);
                return operation;
            }
            catch (Exception ex)
            {
               
                _logger.LogError(ex, "Error al actualizar el carrito");
                operation.IsSuccess = false;
                operation.Message = "Error al actualizar el carrito";

            }
            return operation;
        }

        public async Task<OperationResult> AgregarAsync(SaveCarritoDTO saveCarrito)
        {
            OperationResult operation = new OperationResult();
            try
            {
                _logger.LogInformation("Agregando el carrito", saveCarrito);
                if (saveCarrito is null)
                {
                    _logger.LogError("Se requiere crear un DTO");
                    return operation;
                }
                operation = await _CarritoRepository.AgregarAsync(saveCarrito);
                if (!operation.IsSuccess)
                {
                    _logger.LogError("No se pudo agregar el carrito: {Message}", operation.Message);    
                    return operation;
                }
                _logger.LogInformation("Carrito agregado correctamente para UsuarioId: {UsuarioId}, ProductoId: {ProductoId}, Cantidad: {Cantidad}",
                    saveCarrito.UsuarioId, saveCarrito.ProductoId, saveCarrito.Cantidad);
                return operation;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar el carrito");
                operation.IsSuccess = false;
                operation.Message = "Error al agregar el carrito";

            }
            return operation;
        }

        public async Task<OperationResult> EliminarAsync(RemoveCarritoDTO EliminarCarrito)
        {
            OperationResult operation = new OperationResult();
            try
            {
                _logger.LogInformation("Eliminando el carrito", EliminarCarrito);
                if (EliminarCarrito is null)
                {
                   _logger.LogError("Se requiere crear un DTO");
                    return operation;
                }
                operation = await _CarritoRepository.EliminarAsync(EliminarCarrito);
                if (!operation.IsSuccess)
                {
                   _logger.LogError("No se pudo eliminar el carrito: {Message}", operation.Message);    
                    return operation;
                }
                _logger.LogInformation("Carrito eliminado correctamente para UsuarioId: {UsuarioId}, ProductoId: {ProductoId}",
                    EliminarCarrito.UsuarioId, EliminarCarrito.ProductoId);
                return operation;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el carrito");
                operation.IsSuccess = false;
                operation.Message = "Error al eliminar el carrito";

            }
            return operation;
        }

        public async Task<OperationResult> ObtenerPorIdAsync(int ObtenerCarritoDTO)
        {
            OperationResult operation = new OperationResult();

            try
            {
                _logger.LogInformation("Obteniendo el carrito por ID: {Id}", ObtenerCarritoDTO);
                if (ObtenerCarritoDTO <= 0)
                {
                   _logger.LogError("El ID del carrito debe ser mayor que cero");
                    return operation;
                }
                operation = await _CarritoRepository.ObtenerPorIdAsync(ObtenerCarritoDTO);
                if (!operation.IsSuccess)
                {
                    _logger.LogError("No se pudo obtener el carrito por ID: {Id}, Error: {Message}", ObtenerCarritoDTO, operation.Message);
                    return operation;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el carrito por ID");
                operation.IsSuccess = false;
                operation.Message = "Error al obtener el carrito por ID";
            }
            return operation;
        }

        public async Task<OperationResult> ObtenerTodosAsync()
        {
           OperationResult operation = new OperationResult();
            try
            {
               operation = await _CarritoRepository.ObtenerTodosAsync();
                if (!operation.IsSuccess)
                {
                    _logger.LogError("No se pudo obtener todos los carritos: {Message}", operation.Message);
                    return operation;
                }
                _logger.LogInformation("Carritos obtenidos correctamente");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener todos los carritos {ex.Message}");
                operation = OperationResult.Failure($"Error al obtener todos los carritos {ex.Message}");
            }
            return operation;
        }
    }
}
