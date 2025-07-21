using SellPoint.Domain.Base;
using SellPoint.Aplication.Validations.Mensajes;

namespace SellPoint.Aplication.Validations.PedidoValidator
{
    public static class PedidoMontosValidator
    {
        public static OperationResult ValidarMontos(decimal subtotal, decimal descuento, decimal costoEnvio, decimal total)
        {
            if (subtotal < 0)
                return OperationResult.Failure(MensajesValidacion.SubtotalNegativo);

            if (descuento < 0)
                return OperationResult.Failure(MensajesValidacion.DescuentoNegativo);

            if (costoEnvio < 0)
                return OperationResult.Failure(MensajesValidacion.CostoEnvioNegativo);

            var totalCalculado = subtotal - descuento + costoEnvio;

            if (Math.Abs(total - totalCalculado) > 0.01m)
                return OperationResult.Failure(MensajesValidacion.TotalInconsistente);

            return OperationResult.Success();
        }
    }
}