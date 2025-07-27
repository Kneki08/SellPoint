namespace SellPoint.View.Validations
{
    public static class PedidoMontosValidator
    {
        public static (bool IsSuccess, string Message) Validar(decimal subtotal, decimal descuento, decimal costoEnvio, decimal total)
        {
            if (subtotal < 0)
                return (false, MensajesValidacion.SubtotalInvalido);

            if (descuento < 0)
                return (false, MensajesValidacion.DescuentoInvalido);

            if (costoEnvio < 0)
                return (false, MensajesValidacion.CostoEnvioInvalido);

            if (total < 0)
                return (false, MensajesValidacion.TotalInvalido);

            return (true, "");
        }
    }
}