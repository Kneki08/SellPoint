using Microsoft.Extensions.Logging;
using SellPoint.View.Models.Pedido;
using System;

namespace SellPoint.View.Validations
{
    public class PedidoValidator : IPedidoValidator
    {
        private readonly ILogger<PedidoValidator> _logger;

        private static readonly string[] EstadosValidos = { "EnPreparacion", "Enviado", "Entregado", "Cancelado" };

        public PedidoValidator(ILogger<PedidoValidator> logger)
        {
            _logger = logger;
        }

        public (bool valido, string mensaje) ValidarGuardar(SavePedidoDTO dto)
        {
            return ValidarBase(dto);
        }

        public (bool valido, string mensaje) ValidarActualizar(UpdatePedidoDTO dto)
        {
            if (dto.Id <= 0)
                return Log(MensajesValidacion.IdInvalido);

            var validacionBase = ValidarBase(dto);
            if (!validacionBase.valido)
                return validacionBase;

            if (dto.FechaActualizacion < dto.FechaPedido)
                return Log(MensajesValidacion.FechaActualizacionInvalida);

            return (true, "");
        }

        public (bool valido, string mensaje) ValidarEliminar(int id)
        {
            if (id <= 0)
                return Log(MensajesValidacion.IdInvalido);

            return (true, "");
        }

        public (bool valido, string mensaje) ValidarId(int id)
        {
            if (id <= 0)
                return Log(MensajesValidacion.IdInvalido);

            return (true, "");
        }

        private (bool valido, string mensaje) ValidarBase(BasePedidoDTO dto)
        {
            if (dto.IdUsuario <= 0)
                return Log(MensajesValidacion.UsuarioInvalido);

            if (dto.IdDireccionEnvio <= 0)
                return Log(MensajesValidacion.DireccionInvalida);

            if (string.IsNullOrWhiteSpace(dto.MetodoPago))
                return Log(MensajesValidacion.MetodoPagoVacio);

            if (!EstadosValidos.Contains(dto.Estado))
                return Log(MensajesValidacion.EstadoInvalido);

            if (dto.FechaPedido > DateTime.Now)
                return Log(MensajesValidacion.FechaPedidoInvalida);

            if (dto.Subtotal < 0)
                return Log(MensajesValidacion.SubtotalInvalido);

            if (dto.Descuento < 0)
                return Log(MensajesValidacion.DescuentoInvalido);

            if (dto.CostoEnvio < 0)
                return Log(MensajesValidacion.CostoEnvioInvalido);

            if (dto.Total < 0)
                return Log(MensajesValidacion.TotalInvalido);

            if (dto is SavePedidoDTO save && !string.IsNullOrEmpty(save.Notas) && save.Notas.Length > 500)
                return Log(MensajesValidacion.NotasMuyLargas);

            if (dto is UpdatePedidoDTO update && !string.IsNullOrEmpty(update.Notas) && update.Notas.Length > 500)
                return Log(MensajesValidacion.NotasMuyLargas);

            if (dto.CuponId is not null && dto.CuponId <= 0)
                return Log(MensajesValidacion.CuponIdInvalido);

            return (true, "");
        }

        private (bool, string) Log(string mensaje)
        {
            _logger.LogWarning("Error de validación en Pedido: {Mensaje}", mensaje);
            return (false, mensaje);
        }
    }
}