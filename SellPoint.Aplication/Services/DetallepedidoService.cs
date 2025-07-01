using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Dtos.DetallePedido;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Aplication.Interfaces.Servicios;
using SellPoint.Domain.Base;

namespace SellPoint.Aplication.Services.DetallepedidoService
{
    public sealed class DetallepedidoService : IDetallepedidoService
    {
        private readonly IDetallePedidoRepository _DetallePedidoRepository;
        private readonly ILogger<DetallepedidoService> _logger;
        private readonly IConfiguration _configuration;
      

        public DetallepedidoService(IDetallePedidoRepository DetallepedidoRepository, ILogger<DetallepedidoService> logger, IConfiguration Configuration)
        {
            _DetallePedidoRepository = DetallepedidoRepository;
            _logger = logger;
            _configuration = Configuration;
        }
       
        public async Task<OperationResult> AgregarAsync(SaveDetallePedidoDTO saveDetallepedido)
        {
            OperationResult operation = new OperationResult();
            try
            {
                _logger.LogInformation("Agregando el detalle del pedido", saveDetallepedido);
                if (saveDetallepedido is null)
                {
                    _logger.LogError("Se requiere crear un DTO");
                    return operation;
                }
                operation = await _DetallePedidoRepository.AgregarAsync(saveDetallepedido);
                if (!operation.IsSuccess)
                {
                    _logger.LogError("No se pudo agregar el detalle pedido: {Message}", operation.Message);
                    return operation;
                }
                _logger.LogInformation("Detalle del pedido agregado correctamente para PedidoId: {PedidoId}, ProductoId: {ProductoId}, Cantidad: {Cantidad}",
                    saveDetallepedido.PedidoId, saveDetallepedido.ProductoId, saveDetallepedido.Cantidad);
                return operation;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar el detalle del pedido");
                operation.IsSuccess = false;
                operation.Message = "Error al agregar el detalle del pedido";

            }
            return operation;
        }

        public async Task<OperationResult> EliminarAsync(RemoveDetallePedidoDTO removeDetallePedido)
        {
            OperationResult operation = new OperationResult();
            try
            {
                _logger.LogInformation("Eliminando el detalle del pedido", removeDetallePedido);
                if (removeDetallePedido is null)
                {
                    _logger.LogError("Se requiere crear un DTO");
                    return operation;
                }
                operation = await _DetallePedidoRepository.EliminarAsync(removeDetallePedido);
                if (!operation.IsSuccess)
                {
                    _logger.LogError("No se pudo eliminar el detalle del pedido: {Message}", operation.Message);
                    return operation;
                }
                _logger.LogInformation("Detalle del pedido eliminado correctamente para Id: {Id}",
                    removeDetallePedido.Id);
                return operation;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el detalle del pedido");
                operation.IsSuccess = false;
                operation.Message = "Error al eliminar el detalle del pedido";

            }
            return operation;
        }
        public async Task<OperationResult> ObtenerTodosAsync()
        {
            OperationResult operation = new OperationResult();
            try
            {
                operation = await _DetallePedidoRepository.ObtenerTodosAsync();
                if (!operation.IsSuccess)
                {
                    _logger.LogError("No se pudo obtener todos los detalles del pedido: {Message}", operation.Message);
                    return operation;
                }
                _logger.LogInformation("Detalles obtenidos correctamente");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener todos los detalles {ex.Message}");
                operation = OperationResult.Failure($"Error al obtener todos los detalles {ex.Message}");
            }
            return operation;
        }
        public async Task<OperationResult> ObtenerPorIdAsync(int ObtenerDetallepedidoDTO)
        {
            OperationResult operation = new OperationResult();

            try
            {
                _logger.LogInformation("Obteniendo el detalle del pedido por ID: {Id}", ObtenerDetallepedidoDTO);
                if (ObtenerDetallepedidoDTO <= 0)
                {
                    _logger.LogError("El ID del pedido debe ser mayor que cero");
                    return operation;
                }
                operation = await _DetallePedidoRepository.ObtenerPorIdAsync(ObtenerDetallepedidoDTO);
                if (!operation.IsSuccess)
                {
                    _logger.LogError("No se pudo obtener el detalle del pedido por ID: {Id}, Error: {Message}", ObtenerDetallepedidoDTO, operation.Message);
                    return operation;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el detalle del pedido por ID");
                operation.IsSuccess = false;
                operation.Message = "Error al obtener el detalle del pedido por ID";
            }
            return operation;
        }

        public async Task<OperationResult> ActualizarAsync(UpdateDetallePedidoDTO updateDetallePedido)
        {
            OperationResult operation = new OperationResult();

            try
            {
                _logger.LogInformation("Actualizando el detalle del pedido", updateDetallePedido);

                if (updateDetallePedido is null)
                {
                    _logger.LogError("Se requiere crear un DTO");
                    return operation;
                }
                operation = await _DetallePedidoRepository.ActualizarAsync(updateDetallePedido);

                if (!operation.IsSuccess)
                {
                    _logger.LogError("No se pudo actualizar el detalle del pedido: {Message}", operation.Message);
                    return operation;
                }

                _logger.LogInformation("Detalle del pedido actualizado correctamente para PedidoId: {PedidoId}, ProductoId: {ProductoId}, NuevaCantidad: {NuevaCantidad}, MontoPagar: {PrecioUnitario}",
                    updateDetallePedido.PedidoId, updateDetallePedido.ProductoId, updateDetallePedido.Cantidad, updateDetallePedido.PrecioUnitario);
                return operation;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error al actualizar el detalle del pedido");
                operation.IsSuccess = false;
                operation.Message = "Error al actualizar el detalle del pedido";

            }
            return operation;
        }


    }
}
