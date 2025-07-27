using SellPoint.View.Models.Pedido;

namespace SellPoint.View.Validations
{
    public static class SavePedidoValidator
    {
        private static readonly string[] EstadosValidos = { "EnPreparacion", "Enviado", "Entregado", "Cancelado" };

        public static (bool IsSuccess, string Message) Validar(BasePedidoDTO dto)
        {
            if (dto.IdUsuario <= 0)
                return (false, MensajesValidacion.UsuarioInvalido);

            if (dto.IdDireccionEnvio <= 0)
                return (false, MensajesValidacion.DireccionInvalida);

            if (string.IsNullOrWhiteSpace(dto.MetodoPago))
                return (false, MensajesValidacion.MetodoPagoVacio);

            if (!EstadosValidos.Contains(dto.Estado))
                return (false, MensajesValidacion.EstadoInvalido);

            if (dto.FechaPedido > DateTime.Now)
                return (false, MensajesValidacion.FechaPedidoInvalida);

            var montos = PedidoMontosValidator.Validar(dto.Subtotal, dto.Descuento, dto.CostoEnvio, dto.Total);
            if (!montos.IsSuccess)
                return montos;

            if (dto is SavePedidoDTO save && !string.IsNullOrEmpty(save.Notas) && save.Notas.Length > 500)
                return (false, MensajesValidacion.NotasMuyLargas);

            if (dto.CuponId != null && dto.CuponId <= 0)
                return (false, MensajesValidacion.CuponIdInvalido);

            return (true, "");
        }

        public static (bool IsSuccess, string Message) Validar(SavePedidoDTO dto)
        {
            return Validar(dto as BasePedidoDTO);
        }   
        public static (bool IsSuccess, string Message) Validar(UpdatePedidoDTO dto)
        {
            return Validar(dto as BasePedidoDTO);
        }
    }
}