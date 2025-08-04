using SellPoint.View.Models.Pedido;

namespace SellPoint.View.Validations
{
    public record PedidoCamposResult
    (
        bool Success,
        string Message,
        int IdUsuario,
        int IdDireccion,
        decimal Subtotal,
        decimal Descuento,
        decimal CostoEnvio,
        decimal Total
    );

    public static class PedidoCamposParser
    {
        public static PedidoCamposResult TryParseCampos(
            string idUsuario, string idDireccion,
            string subtotal, string descuento,
            string costoEnvio, string total)
        {
            if (!int.TryParse(idUsuario, out int idUsr))
                return Error(MensajesValidacion.ErrorIdUsuario);

            if (!int.TryParse(idDireccion, out int idDir))
                return Error(MensajesValidacion.ErrorIdDireccion);

            if (!decimal.TryParse(subtotal, out decimal subTot))
                return Error(MensajesValidacion.ErrorSubtotal);

            if (!decimal.TryParse(descuento, out decimal desc))
                return Error(MensajesValidacion.ErrorDescuento);

            if (!decimal.TryParse(costoEnvio, out decimal costEnv))
                return Error(MensajesValidacion.ErrorCostoEnvio);

            if (!decimal.TryParse(total, out decimal tot))
                return Error(MensajesValidacion.ErrorTotal);

            return new(true, "", idUsr, idDir, subTot, desc, costEnv, tot);
        }

        private static PedidoCamposResult Error(string mensaje) =>
            new(false, mensaje, 0, 0, 0, 0, 0, 0);
    }
}