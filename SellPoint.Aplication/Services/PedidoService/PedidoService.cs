using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Dtos.Pedido;
using SellPoint.Aplication.Interfaces.IService;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;

namespace SellPoint.Aplication.Services.PedidoService
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly ILogger<PedidoService> _logger;
        private readonly IConfiguration _configuration;

        public PedidoService(
            IPedidoRepository pedidoRepository,
            ILogger<PedidoService> logger,
            IConfiguration configuration)
        {
            _pedidoRepository = pedidoRepository;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> ObtenerTodosAsync()
        {
            try
            {
                _logger.LogInformation("Obteniendo todos los pedidos...");
                var result = await _pedidoRepository.ObtenerTodosAsync();

                if (!result.IsSuccess)
                    _logger.LogWarning("No se pudieron obtener los pedidos: {Message}", result.Message);
                else
                    _logger.LogInformation("Pedidos obtenidos correctamente.");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los pedidos");
                return OperationResult.Failure("Error al obtener los pedidos: " + ex.Message);
            }
        }

        public async Task<OperationResult> ObtenerPorIdAsync(int id)
        {
            if (id <= 0)
                return OperationResult.Failure("El ID del pedido debe ser mayor que cero.");

            try
            {
                _logger.LogInformation("Buscando pedido con ID: {Id}", id);
                var result = await _pedidoRepository.ObtenerPorIdAsync(id);

                if (!result.IsSuccess)
                    _logger.LogWarning("No se encontró el pedido: {Message}", result.Message);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el pedido con ID {Id}", id);
                return OperationResult.Failure("Error al obtener el pedido: " + ex.Message);
            }
        }

        public async Task<OperationResult> AgregarAsync(SavePedidoDTO savePedido)
        {
            if (savePedido is null)
                return OperationResult.Failure("La entidad no puede ser nula.");

            if (savePedido.UsuarioId <= 0)
                return OperationResult.Failure("El ID del usuario debe ser mayor que cero.");

            if (savePedido.Subtotal < 0)
                return OperationResult.Failure("El subtotal no puede ser negativo.");

            if (savePedido.Descuento < 0)
                return OperationResult.Failure("El descuento no puede ser negativo.");

            if (savePedido.CostoEnvio < 0)
                return OperationResult.Failure("El costo de envío no puede ser negativo.");

            if (savePedido.Total <= 0)
                return OperationResult.Failure("El total del pedido debe ser mayor que cero.");

            if (savePedido.Total != (savePedido.Subtotal - savePedido.Descuento + savePedido.CostoEnvio))
                return OperationResult.Failure("El total del pedido no coincide con la suma de subtotal, descuento y costo de envío.");

            if (string.IsNullOrWhiteSpace(savePedido.MetodoPago))
                return OperationResult.Failure("El método de pago es obligatorio.");

            if (savePedido.MetodoPago!.Length > 50)
                return OperationResult.Failure("El método de pago no debe superar los 50 caracteres.");

            if (!string.IsNullOrWhiteSpace(savePedido.ReferenciaPago) && savePedido.ReferenciaPago.Length > 100)
                return OperationResult.Failure("La referencia de pago no debe superar los 100 caracteres.");

            if (!string.IsNullOrWhiteSpace(savePedido.Notas) && savePedido.Notas.Length > 500)
                return OperationResult.Failure("Las notas no deben superar los 500 caracteres.");

            try
            {
                _logger.LogInformation("Registrando nuevo pedido para UsuarioId: {UsuarioId}", savePedido.UsuarioId);
                var result = await _pedidoRepository.AgregarAsync(savePedido);

                if (!result.IsSuccess)
                    _logger.LogWarning("No se pudo registrar el pedido: {Message}", result.Message);
                else
                    _logger.LogInformation("Pedido registrado correctamente.");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al registrar el pedido");
                return OperationResult.Failure("Error al registrar el pedido: " + ex.Message);
            }
        }

        public async Task<OperationResult> ActualizarAsync(UpdatePedidoDTO updatePedido)
        {
            if (updatePedido is null)
                return OperationResult.Failure("La entidad no puede ser nula.");

            if (updatePedido.Id <= 0)
                return OperationResult.Failure("El ID del pedido debe ser mayor que cero.");

            if (string.IsNullOrWhiteSpace(updatePedido.Estado))
                return OperationResult.Failure("El estado del pedido es obligatorio.");

            var estadosValidos = new[] { "Pendiente", "Pagado", "Cancelado", "Enviado" };
            if (!estadosValidos.Contains(updatePedido.Estado))
                return OperationResult.Failure("El estado del pedido no es válido.");

            if (string.IsNullOrWhiteSpace(updatePedido.MetodoPago))
                return OperationResult.Failure("El método de pago es obligatorio.");

            if (updatePedido.MetodoPago!.Length > 50)
                return OperationResult.Failure("El método de pago no debe superar los 50 caracteres.");

            if (string.IsNullOrWhiteSpace(updatePedido.ReferenciaPago))
                return OperationResult.Failure("La referencia de pago es obligatoria.");

            if (updatePedido.ReferenciaPago!.Length > 100)
                return OperationResult.Failure("La referencia de pago no debe superar los 100 caracteres.");

            if (!string.IsNullOrWhiteSpace(updatePedido.Notas) && updatePedido.Notas.Length > 500)
                return OperationResult.Failure("Las notas no deben superar los 500 caracteres.");

            if (updatePedido.FechaActualizacion == DateTime.MinValue)
                return OperationResult.Failure("La fecha de actualización no es válida.");

            try
            {
                _logger.LogInformation("Actualizando pedido con ID: {Id}", updatePedido.Id);
                var result = await _pedidoRepository.ActualizarAsync(updatePedido);

                if (!result.IsSuccess)
                    _logger.LogWarning("No se pudo actualizar el pedido: {Message}", result.Message);
                else
                    _logger.LogInformation("Pedido actualizado correctamente.");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el pedido");
                return OperationResult.Failure("Error al actualizar el pedido: " + ex.Message);
            }
        }

        public async Task<OperationResult> EliminarAsync(RemovePedidoDTO removePedido)
        {
            if (removePedido is null)
                return OperationResult.Failure("La entidad no puede ser nula.");

            if (removePedido.Id <= 0)
                return OperationResult.Failure("El ID del pedido debe ser mayor que cero.");

            try
            {
                _logger.LogInformation("Eliminando pedido con ID: {Id}", removePedido.Id);
                var result = await _pedidoRepository.EliminarAsync(removePedido);

                if (!result.IsSuccess)
                    _logger.LogWarning("No se pudo eliminar el pedido: {Message}", result.Message);
                else
                    _logger.LogInformation("Pedido eliminado correctamente.");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el pedido");
                return OperationResult.Failure("Error al eliminar el pedido: " + ex.Message);
            }
        }
    }
}