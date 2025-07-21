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
            if (dto == null)
                return OperationResult.Failure("El DTO no puede ser nulo.");

            try
            {
                var entidad = new DetallePedido
                {
                    Pedidoid = dto.PedidoId,
                    ProductoId = dto.ProductoId,
                    Cantidad = dto.Cantidad,
                    PrecioUnitario = dto.PrecioUnitario
                };

                return await _detallePedidoRepository.AddAsync(entidad);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar el detalle del pedido.");
                return OperationResult.Failure("Error interno.");
            }
        }

        public async Task<OperationResult> UpdateAsync(UpdateDetallePedidoDTO dto)
        {
            if (dto == null)
                return OperationResult.Failure("La entidad no puede ser nula.");

            try
            {
                var entidad = new DetallePedido
                {
                    Pedidoid = dto.PedidoId,
                    ProductoId = dto.ProductoId,
                    Cantidad = dto.Cantidad,
                    PrecioUnitario = dto.PrecioUnitario
                };

                return await _detallePedidoRepository.UpdateAsync(entidad);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el detalle del pedido.");
                return OperationResult.Failure("Error interno.");
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
                _logger.LogError(ex, "Error al eliminar el detalle del pedido.");
                return OperationResult.Failure("Error interno.");
            }
        }

        public async Task<OperationResult> GetByIdAsync(int id)
        {
            if (id <= 0)
                return OperationResult.Failure("El Id debe ser mayor que cero.");

            try
            {
                var result = await _detallePedidoRepository.GetByIdAsync(id);
                if (!result.IsSuccess || result.Data is not DetallePedido entidad)
                    return OperationResult.Failure("Detalle no encontrado.");

                var dto = new DetallePedidoDTO
                {
                    PedidoId = entidad.Pedidoid,
                    ProductoId = entidad.ProductoId,
                    Cantidad = entidad.Cantidad,
                };

                return OperationResult.Success(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el detalle del pedido por ID.");
                return OperationResult.Failure("Error interno.");
            }
        }

        public async Task<OperationResult> GetAllAsync()
        {
            try
            {
                var result = await _detallePedidoRepository.GetAllAsync();
                if (!result.IsSuccess || result.Data is not IEnumerable<DetallePedido> lista)
                    return OperationResult.Failure("No se encontraron datos.");

                var dtoList = lista.Select(dp => new DetallePedidoDTO
                {
                    PedidoId = dp.Pedidoid,
                    ProductoId = dp.ProductoId,
                    Cantidad = dp.Cantidad,
                }).ToList();

                return OperationResult.Success(dtoList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los detalles del pedido.");
                return OperationResult.Failure("Error interno.");
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
