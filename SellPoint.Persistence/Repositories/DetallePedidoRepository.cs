using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;
using SellPoint.Domain.Entities.Orders;
using SellPoint.Persistence.Context;
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
                    return OperationResult.Failure("Detalle no encontrado.");

                return OperationResult.Success(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener por ID");
                return OperationResult.Failure("Error al obtener por ID.");
            }
        }

        public async Task<OperationResult> GetAllAsync(Expression<Func<DetallePedido, bool>>? filter = null)
        {
            try
            {
                var query = _context.DetallePedido.AsQueryable();

                if (filter != null)
                    query = query.Where(filter);

                var result = await query.ToListAsync();

                return OperationResult.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los detalles");
                return OperationResult.Failure("Error al obtener todos los detalles.");
            }
        }

        public async Task<OperationResult> AddAsync(DetallePedido entity)
        {
            try
            {
                await _context.DetallePedido.AddAsync(entity);
                await _context.SaveChangesAsync();
                return OperationResult.Success("Detalle agregado correctamente.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar");
                return OperationResult.Failure("Error al agregar detalle.");
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
                _logger.LogError(ex, "Error al actualizar");
                return OperationResult.Failure("Error al actualizar.");
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
