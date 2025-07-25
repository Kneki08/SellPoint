using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Dtos.DetallePedido;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Aplication.Interfaces.Servicios;
using SellPoint.Domain.Base;
using SellPoint.Domain.Entities.Orders;

namespace SellPoint.Aplication.Services.DetallepedidoService
{
    public sealed class DetallepedidoService : IDetallepedidoService
    {
        private readonly IDetallepedidoRepository _detallePedidoRepository;
        private readonly ILogger<DetallepedidoService> _logger;
        private readonly IConfiguration _configuration;

        public DetallepedidoService(
            IDetallepedidoRepository detallepedidoRepository,
            ILogger<DetallepedidoService> logger,
            IConfiguration configuration)
        {
            _detallePedidoRepository = detallepedidoRepository;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> AddAsync(SaveDetallePedidoDTO dto)
        {
            try
            {
                if (dto == null)
                    return OperationResult.Failure("El DTO no puede ser nulo.");
               

                var entidad = new DetallePedido
                {
                    Pedidoid = dto.PedidoId,
                    ProductoId = dto.ProductoId,
                    Cantidad = dto.Cantidad,
                    PrecioUnitario = dto.PrecioUnitario
                  
                };

                var result = await _detallePedidoRepository.AddAsync(entidad);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR INTERNO: {ex.Message}");
                _logger.LogError(ex, "Error al agregar detalle");
                return OperationResult.Failure($"Error al agregar detalle: {ex.Message}");
                
            }
        }

        public async Task<OperationResult> UpdateAsync(UpdateDetallePedidoDTO dto)
        {
            if (dto == null || dto.Id <= 0)
                return OperationResult.Failure("La entidad no puede ser nula.");

            try
            {
                var entidad = new DetallePedido
                {
                    Id = dto.Id,
                    Pedidoid = dto.PedidoId,
                    ProductoId = dto.ProductoId,
                    Cantidad = dto.Cantidad,
                    PrecioUnitario = dto.PrecioUnitario
                };

                return await _detallePedidoRepository.UpdateAsync(entidad);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar el detalle: {ex.Message}");
                return OperationResult.Failure($"Error: {ex.Message}");
            }
        }

        public async Task<OperationResult> DeleteAsync(RemoveDetallePedidoDTO dto)
        {
            if (dto == null || dto.Id <= 0)
                return OperationResult.Failure("El Id debe ser mayor que cero.");

            try
            {
                return await _detallePedidoRepository.DeleteAsync(dto.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar el detalle: {ex.Message}");
                return OperationResult.Failure($"Error: {ex.Message}");
            }
        }

        public async Task<OperationResult> GetByIdAsync(int id)
        {
            if (id <= 0)
                return OperationResult.Failure("El Id debe ser mayor que cero.");

            try
            {
                var result = await _detallePedidoRepository.GetByIdAsync(id);
                if (!result.IsSuccess)
                    return result; // Deja que el repositorio maneje el mensaje

                if (result.Data is not DetallePedido entidad)
                    return OperationResult.Success(null); // Devolver 200 con data: null


                var dto = new DetallePedidoDTO
                {
                    Id = entidad.Id,
                    PedidoId = entidad.Pedidoid,
                    ProductoId = entidad.ProductoId,
                    Cantidad = entidad.Cantidad,
                    PrecioUnitario = entidad.PrecioUnitario
                };

                return OperationResult.Success(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener por id el detalle: {ex.Message}");
                return OperationResult.Failure($"Error: {ex.Message}");
            }
        }

        public async Task<OperationResult> GetAllAsync()
        {
            try
            {
                var lista = await _detallePedidoRepository.GetAllAsync();

                return OperationResult.Success(lista);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los detalles del pedido.");
                return OperationResult.Failure("Error al obtener los detalles del pedido.");
            }
            try
            {
                await _detallePedidoRepository.GetAllAsync();
                var lista = new List<DetallePedidoDTO>(); // simula que no hay datos
                return OperationResult.Success(lista);
            }
            catch
            {
                return OperationResult.Failure("Error");
            }
        }
        public async Task<OperationResult> SaveChangesAsync()
        {
            try
            {
                await _detallePedidoRepository.SaveChangesAsync();
                return OperationResult.Success("Cambios guardados correctamente.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar los cambios");
                return OperationResult.Failure("Error al guardar los cambios.");
            }
        }
    }
}

