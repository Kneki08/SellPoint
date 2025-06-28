using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Dtos.Cupon;
using SellPoint.Aplication.Interfaces.IService;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Aplication.Services.CuponService
{
    public class CuponService : ICuponService
    {
        private readonly ICuponRepository _cuponRepository;
        private readonly ILogger<CuponService> _logger;
        private readonly IConfiguration _configuration;

        public CuponService(
            ICuponRepository cuponRepository,
            ILogger<CuponService> logger,
            IConfiguration configuration)
        {
            _cuponRepository = cuponRepository;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> ObtenerTodosAsync()
        {
            OperationResult operationResult = OperationResult.Success();

            try
            {
                _logger.LogInformation("Obteniendo todos los cupones...");
                operationResult = await _cuponRepository.ObtenerTodosAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener los cupones: {ex.Message}");
                operationResult = OperationResult.Failure($"Error al obtener los cupones: {ex.Message}");
            }

            return operationResult;
        }

        public async Task<OperationResult> ObtenerPorIdAsync(int cuponId)
        {
            OperationResult operationResult = OperationResult.Success();

            if (cuponId <= 0)
                return OperationResult.Failure("El Id del cupón debe ser mayor que cero.");

            try
            {
                _logger.LogInformation($"Buscando cupón con Id: {cuponId}");
                operationResult = await _cuponRepository.ObtenerPorIdAsync(cuponId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el cupón con Id {cuponId}: {ex.Message}");
                operationResult = OperationResult.Failure($"Error al obtener el cupón: {ex.Message}");
            }

            return operationResult;
        }

        public async Task<OperationResult> CrearAsync(SaveCuponDTO nuevoCupon)
        {
            OperationResult operationResult = OperationResult.Success();

            if (nuevoCupon is null)
                return OperationResult.Failure("La entidad no puede ser nula.");

            if (string.IsNullOrWhiteSpace(nuevoCupon.Codigo))
                return OperationResult.Failure("El código del cupón es obligatorio.");

            try
            {
                _logger.LogInformation($"Creando cupón con código: {nuevoCupon.Codigo}");
                operationResult = await _cuponRepository.AgregarAsync(nuevoCupon);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al crear el cupón '{nuevoCupon.Codigo}': {ex.Message}");
                operationResult = OperationResult.Failure($"Error al crear el cupón: {ex.Message}");
            }

            return operationResult;
        }

        public async Task<OperationResult> ActualizarAsync(UpdateCuponDTO cuponActualizado)
        {
            OperationResult operationResult = OperationResult.Success();

            if (cuponActualizado is null)
                return OperationResult.Failure("La entidad no puede ser nula.");

            if (cuponActualizado.Id <= 0)
                return OperationResult.Failure("El Id del cupón debe ser mayor que cero.");

            if (string.IsNullOrWhiteSpace(cuponActualizado.Codigo))
                return OperationResult.Failure("El código del cupón es obligatorio.");

            try
            {
                _logger.LogInformation($"Actualizando cupón con Id: {cuponActualizado.Id}");
                operationResult = await _cuponRepository.ActualizarAsync(cuponActualizado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar el cupón con Id {cuponActualizado.Id}: {ex.Message}");
                operationResult = OperationResult.Failure($"Error al actualizar el cupón: {ex.Message}");
            }

            return operationResult;
        }

        public async Task<OperationResult> EliminarAsync(RemoveCuponDTIO cuponAEliminar)
        {
            OperationResult operationResult = OperationResult.Success();

            if (cuponAEliminar is null)
                return OperationResult.Failure("La entidad no puede ser nula.");

            if (cuponAEliminar.Id <= 0)
                return OperationResult.Failure("El Id del cupón debe ser mayor que cero.");

            try
            {
                _logger.LogInformation($"Eliminando cupón con Id: {cuponAEliminar.Id}");
                operationResult = await _cuponRepository.EliminarAsync(cuponAEliminar);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar el cupón con Id {cuponAEliminar.Id}: {ex.Message}");
                operationResult = OperationResult.Failure($"Error al eliminar el cupón: {ex.Message}");
            }

            return operationResult;
        }
    }
}
