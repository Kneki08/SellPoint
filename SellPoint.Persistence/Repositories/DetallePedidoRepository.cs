using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Dtos.DetallePedido;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;
using SellPoint.Domain.Entities.Orders;
using SellPoint.Persistence.Context;
using System.Data;
using System.Linq.Expressions;

namespace SellPoint.Persistence.Repositories
{
    public class DetallePedidoRepository : IDetallepedidoRepository
    {
        private readonly SellPointDbContext _context;
        private readonly ILogger<DetallePedidoRepository> _logger;

        public DetallePedidoRepository(SellPointDbContext context, ILogger<DetallePedidoRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Métodos requeridos por IRepository<T>
        public async Task<OperationResult> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _context.DetallePedido.FindAsync(id);
                if (entity == null)
                    return OperationResult.Success(null); // o Failure con código 404 si lo soportas

                return OperationResult.Success(entity);
            }
            catch (Exception ex)
            {
                return OperationResult.Failure("Error al consultar el detalle");
            }
        }

        public async Task<List<DetallePedidoDTO>> GetAllAsync()
        {
                        return await _context.DetallePedido
                       .Where(d => !d.Esta_eliminado)
                       .Select(d => new DetallePedidoDTO
                       {
                           Id = d.Id,
                           Cantidad = d.Cantidad,
                           PrecioUnitario = d.PrecioUnitario,
                           PedidoId = d.Pedidoid,
                           ProductoId = d.ProductoId
                       })
                       .ToListAsync();
           
        }

        public async Task<OperationResult> AddAsync(DetallePedido entity)
        {
            try
            {
                if (entity == null)
                    return OperationResult.Failure("El detalle del pedido no puede ser nulo.");

                await _context.DetallePedido.AddAsync(entity);
                await _context.SaveChangesAsync();

                return OperationResult.Success("Detalle agregado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR INTERNO: {ex.Message}");
                // Esta línea es la más importante
                return OperationResult.Failure($"Error al agregar detalle: {ex.Message}");
            }
        }


        public async Task<OperationResult> UpdateAsync(DetallePedido entity)
        {
            try
            {
                _context.DetallePedido.Update(entity);
                await _context.SaveChangesAsync();
                return OperationResult.Success("Detalle actualizado correctamente.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualiza el detalle: {ex.Message}");
                return OperationResult.Failure($"Error: {ex.Message}");
            }
        }

        public async Task<OperationResult> DeleteAsync(int id)
        {
            try
            {
                var entity = await _context.DetallePedido.FindAsync(id);
                if (entity == null)
                    return OperationResult.Failure("Detalle no encontrado.");

                _context.DetallePedido.Remove(entity);
                await _context.SaveChangesAsync();

                return OperationResult.Success("Detalle eliminado correctamente.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar detalle");
                return OperationResult.Failure("Error al eliminar detalle.");
            }
        }

        public async Task<OperationResult> SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
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
