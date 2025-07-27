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
        string idUsuario, string idDireccion, string subtotal, string descuento, string costoEnvio, string total)
        {
            if (!int.TryParse(idUsuario, out int idUsr))
                return new(false, "El ID de usuario debe ser un número válido.", 0, 0, 0, 0, 0, 0);
            if (!int.TryParse(idDireccion, out int idDir))
                return new(false, "La dirección debe ser un número válido.", 0, 0, 0, 0, 0, 0);
            if (!decimal.TryParse(subtotal, out decimal subTot))
                return new(false, "El subtotal debe ser un número decimal válido.", 0, 0, 0, 0, 0, 0);
            if (!decimal.TryParse(descuento, out decimal desc))
                return new(false, "El descuento debe ser un número decimal válido.", 0, 0, 0, 0, 0, 0);
            if (!decimal.TryParse(costoEnvio, out decimal costEnv))
                return new(false, "El costo de envío debe ser un número decimal válido.", 0, 0, 0, 0, 0, 0);
            if (!decimal.TryParse(total, out decimal tot))
                return new(false, "El total debe ser un número decimal válido.", 0, 0, 0, 0, 0, 0);

            return new(true, "", idUsr, idDir, subTot, desc, costEnv, tot);
        }
    }
}