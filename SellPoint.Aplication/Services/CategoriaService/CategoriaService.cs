using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Dtos.Categoria;
using SellPoint.Aplication.Interfaces.IService;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Application.Services.Categoria
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ILogger<CategoriaService> _logger;
        private readonly IConfiguration _configuration;

        public CategoriaService(
            ICategoriaRepository categoriaRepository,
            ILogger<CategoriaService> logger,
            IConfiguration configuration)
        {
            _categoriaRepository = categoriaRepository;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> ObtenerTodosAsync()
        {
            OperationResult operationResult = OperationResult.Success();

            try
            {
                _logger.LogInformation("Obteniendo todas las categorías...");
                operationResult = await _categoriaRepository.ObtenerTodosAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener las categorías: {ex.Message}");
                operationResult = OperationResult.Failure($"Error al obtener las categorías: {ex.Message}");
            }

            return operationResult;
        }

        public async Task<OperationResult> ObtenerPorIdAsync(int categoriaId)
        {
            OperationResult operationResult = OperationResult.Success();

            if (categoriaId <= 0)
                return OperationResult.Failure("El Id de la categoría debe ser mayor que cero.");

            try
            {
                _logger.LogInformation($"Buscando categoría con Id: {categoriaId}");
                operationResult = await _categoriaRepository.ObtenerPorIdAsync(categoriaId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener la categoría con Id {categoriaId}: {ex.Message}");
                operationResult = OperationResult.Failure($"Error al obtener la categoría: {ex.Message}");
            }

            return operationResult;
        }

        public async Task<OperationResult> CrearAsync(SaveCategoriaDTO nuevaCategoria)
        {
            OperationResult operationResult = OperationResult.Success();

            if (nuevaCategoria is null)
                return OperationResult.Failure("La entidad no puede ser nula.");

            if (string.IsNullOrWhiteSpace(nuevaCategoria.Nombre))
                return OperationResult.Failure("El nombre de la categoría es obligatorio.");

            try
            {
                _logger.LogInformation($"Creando categoría: {nuevaCategoria.Nombre}");
                operationResult = await _categoriaRepository.AgregarAsync(nuevaCategoria);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al crear la categoría: {ex.Message}");
                operationResult = OperationResult.Failure($"Error al crear la categoría: {ex.Message}");
            }

            return operationResult;
        }

        public async Task<OperationResult> ActualizarAsync(UpdateCategoriaDTO categoriaActualizada)
        {
            OperationResult operationResult = OperationResult.Success();

            if (categoriaActualizada is null)
                return OperationResult.Failure("La entidad no puede ser nula.");

            if (categoriaActualizada.Id <= 0)
                return OperationResult.Failure("El Id de la categoría debe ser mayor que cero.");

            if (string.IsNullOrWhiteSpace(categoriaActualizada.Nombre))
                return OperationResult.Failure("El nombre de la categoría es obligatorio.");

            try
            {
                _logger.LogInformation($"Actualizando categoría con Id: {categoriaActualizada.Id}");
                operationResult = await _categoriaRepository.ActualizarAsync(categoriaActualizada);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar la categoría con Id {categoriaActualizada.Id}: {ex.Message}");
                operationResult = OperationResult.Failure($"Error al actualizar la categoría: {ex.Message}");
            }

            return operationResult;
        }

        public async Task<OperationResult> EliminarAsync(RemoveCategoriaDTO categoriaAEliminar)
        {
            OperationResult operationResult = OperationResult.Success();

            if (categoriaAEliminar is null)
                return OperationResult.Failure("La entidad no puede ser nula.");

            if (categoriaAEliminar.Id <= 0)
                return OperationResult.Failure("El Id de la categoría debe ser mayor que cero.");

            try
            {
                _logger.LogInformation($"Eliminando categoría con Id: {categoriaAEliminar.Id}");
                operationResult = await _categoriaRepository.EliminarAsync(categoriaAEliminar);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar la categoría con Id {categoriaAEliminar.Id}: {ex.Message}");
                operationResult = OperationResult.Failure($"Error al eliminar la categoría: {ex.Message}");
            }

            return operationResult;
        }
    }
}
