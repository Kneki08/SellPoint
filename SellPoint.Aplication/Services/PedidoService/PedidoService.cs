using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Dtos.Pedido;
using SellPoint.Aplication.Interfaces.IService;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Aplication.Validations.PedidoValidator;
using SellPoint.Aplication.Validations.Mensajes;
using SellPoint.Domain.Base;
using SellPoint.Domainn.Entities.Orders;

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
            const string contexto = "PedidoService.ObtenerTodosAsync";

            try
            {
                _logger.LogInformation("{Contexto} iniciado", contexto);

                var resultado = await _pedidoRepository.ObtenerTodosAsync();

                if (!resultado.IsSuccess)
                {
                    _logger.LogWarning(MensajesValidacion.ErrorObtenerPedidos);
                    return resultado;
                }

                if (resultado.Data is not List<PedidoDTO> pedidos)
                {
                    _logger.LogError("El objeto retornado no es del tipo esperado List<PedidoDTO>");
                    return OperationResult.Failure(MensajesValidacion.ErrorObtenerPedidos);
                }

                _logger.LogInformation("Pedidos obtenidos exitosamente. Total: {Total}", pedidos.Count);
                return OperationResult.Success(pedidos, MensajesValidacion.PedidosObtenidosCorrectamente);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado en {Contexto}", contexto);
                return OperationResult.Failure(MensajesValidacion.ErrorObtenerPedidos);
            }
        }

        public async Task<OperationResult> ObtenerPorIdAsync(int id)
        {
            const string contexto = "PedidoService.ObtenerPorIdAsync";

            var validacion = PedidoValidator.ValidarId(id);
            if (!validacion.IsSuccess)
                return validacion;

            try
            {
                _logger.LogInformation("{Contexto} iniciado para Id: {Id}", contexto, id);

                var resultado = await _pedidoRepository.ObtenerPorIdAsync(id);

                if (!resultado.IsSuccess)
                {
                    _logger.LogWarning(string.Format(MensajesValidacion.PedidoNoEncontrado, id));
                    return resultado;
                }

                if (resultado.Data is not PedidoDTO pedido)
                {
                    _logger.LogError("El objeto retornado no es del tipo esperado PedidoDTO");
                    return OperationResult.Failure(MensajesValidacion.ErrorObtenerPedidos);
                }

                _logger.LogInformation("Pedido obtenido exitosamente: Id {Id}", pedido.Id);
                return OperationResult.Success(pedido, MensajesValidacion.PedidoEncontrado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado en {Contexto}", contexto);
                return OperationResult.Failure(MensajesValidacion.ErrorObtenerPedidos);
            }
        }

        public async Task<OperationResult> AgregarAsync(SavePedidoDTO dto)
        {
            const string contexto = "PedidoService.AgregarAsync";

            var validacion = PedidoValidator.ValidarSave(dto);
            if (!validacion.IsSuccess)
                return validacion;

            try
            {
                var pedido = new Pedido
                {
                    IdUsuario = dto.IdUsuario,
                    Subtotal = dto.Subtotal,
                    Descuento = dto.Descuento,
                    CostoEnvio = dto.CostoEnvio,
                    Total = dto.Total,
                    CuponId = dto.CuponId,
                    DireccionEnvioId = dto.IdDireccionEnvio,
                    MetodoPago = Enum.TryParse<MetodoPago>(dto.MetodoPago, out var metodoPago)
                                 ? metodoPago
                                 : throw new InvalidOperationException(MensajesValidacion.MetodoPagoRequerido),
                    ReferenciaPago = dto.ReferenciaPago,
                    Notas = dto.Notas
                };

                var resultEntidad = PedidoValidator.ValidarEntidad(pedido);
                if (!resultEntidad.IsSuccess)
                    return resultEntidad;

                _logger.LogInformation("{Contexto} iniciado para UsuarioId: {UsuarioId}", contexto, dto.IdUsuario);

                var resultado = await _pedidoRepository.AgregarAsync(pedido);

                if (resultado.IsSuccess)
                    _logger.LogInformation(MensajesValidacion.OperacionExitosa);
                else
                    _logger.LogWarning("Fallo al agregar pedido: {Mensaje}", resultado.Message);

                return resultado;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado en {Contexto}", contexto);
                return OperationResult.Failure(MensajesValidacion.ErrorAgregarPedido);
            }
        }

        public async Task<OperationResult> ActualizarAsync(UpdatePedidoDTO dto)
        {
            const string contexto = "PedidoService.ActualizarAsync";

            var validacion = PedidoValidator.ValidarUpdate(dto);
            if (!validacion.IsSuccess)
                return validacion;

            try
            {

                if (!Enum.TryParse<EstadoPedido>(dto.Estado, ignoreCase: true, out var estado))
                    return OperationResult.Failure(MensajesValidacion.EstadoNoValido);

                if (!Enum.TryParse<MetodoPago>(dto.MetodoPago, ignoreCase: true, out var metodoPago))
                    return OperationResult.Failure(MensajesValidacion.MetodoPagoRequerido);

                var pedido = new Pedido
                {
                    Id = dto.Id,
                    IdUsuario = dto.IdUsuario,
                    Estado = estado,
                    MetodoPago = metodoPago,
                    ReferenciaPago = dto.ReferenciaPago,
                    Notas = dto.Notas,
                    Fecha_actualizacion = dto.FechaActualizacion.ToUniversalTime(),
                    Subtotal = dto.Subtotal,
                    Descuento = dto.Descuento,
                    CostoEnvio = dto.CostoEnvio,
                    Total = dto.Total,
                    CuponId = dto.CuponId,
                    DireccionEnvioId = dto.IdDireccionEnvio
                };

                var resultEntidad = PedidoValidator.ValidarEntidad(pedido, true);
                if (!resultEntidad.IsSuccess)
                    return resultEntidad;

                _logger.LogInformation("{Contexto} iniciado para PedidoId: {Id}", contexto, dto.Id);

                var resultado = await _pedidoRepository.ActualizarAsync(pedido);

                if (resultado.IsSuccess)
                    _logger.LogInformation(MensajesValidacion.OperacionExitosa);
                else
                    _logger.LogWarning("Fallo al actualizar pedido: {Mensaje}", resultado.Message);

                return resultado;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado en {Contexto}", contexto);
                return OperationResult.Failure(MensajesValidacion.ErrorActualizarPedido);
            }
        }

        public async Task<OperationResult> EliminarAsync(RemovePedidoDTO dto)
        {
            const string contexto = "PedidoService.EliminarAsync";

            if (dto == null)
            {
                _logger.LogWarning("{Contexto} falló: el DTO es nulo", contexto);
                return OperationResult.Failure(MensajesValidacion.EntidadNula);
            }

            _logger.LogInformation("{Contexto} iniciado para Id: {Id}", contexto, dto.Id);

            var validacion = PedidoValidator.ValidarRemove(dto);
            if (!validacion.IsSuccess)
            {
                _logger.LogWarning("Validación fallida: {Mensaje}", validacion.Message);
                return validacion;
            }

            try
            {
                var resultado = await _pedidoRepository.EliminarAsync(dto);

                if (!resultado.IsSuccess)
                    _logger.LogWarning("No se pudo eliminar el pedido Id: {Id}. Detalle: {Mensaje}", dto.Id, resultado.Message);
                else
                    _logger.LogInformation(MensajesValidacion.OperacionExitosa);

                return resultado;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado en {Contexto} para Id: {Id}", contexto, dto.Id);
                return OperationResult.Failure(MensajesValidacion.ErrorEliminarPedido);
            }
        }
    }
}