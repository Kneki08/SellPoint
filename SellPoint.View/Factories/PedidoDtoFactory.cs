using SellPoint.View.Models.Pedido;
using SellPoint.View.Validations;

namespace SellPoint.View.Factories
{
    public class PedidoDtoFactory : IPedidoDtoFactory
    {
        public SavePedidoDTO CrearSaveDTO(PedidoCamposResult campos, string metodoPago, string referenciaPago, string estado, string notas)
        {
            return new SavePedidoDTO
            {
                IdUsuario = campos.IdUsuario,
                IdDireccionEnvio = campos.IdDireccion,
                MetodoPago = metodoPago,
                ReferenciaPago = referenciaPago,
                Subtotal = campos.Subtotal,
                Descuento = campos.Descuento,
                CostoEnvio = campos.CostoEnvio,
                Total = campos.Total,
                Estado = estado,
                FechaPedido = DateTime.Now,
                CuponId = null,
                Notas = notas
            };
        }

        public UpdatePedidoDTO CrearUpdateDTO(PedidoCamposResult campos, PedidoDTO selected, string metodoPago, string referenciaPago, string estado, string notas)
        {
            return new UpdatePedidoDTO
            {
                Id = selected.Id,
                IdUsuario = campos.IdUsuario,
                IdDireccionEnvio = campos.IdDireccion,
                MetodoPago = metodoPago,
                ReferenciaPago = referenciaPago,
                Subtotal = campos.Subtotal,
                Descuento = campos.Descuento,
                CostoEnvio = campos.CostoEnvio,
                Total = campos.Total,
                Estado = estado,
                FechaPedido = selected.FechaPedido,
                FechaActualizacion = DateTime.Now,
                CuponId = null,
                Notas = notas
            };
        }
    }
}