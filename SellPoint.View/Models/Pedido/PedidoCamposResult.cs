namespace SellPoint.View.Models.Pedido
{
    public record PedidoCamposResult(
        bool Success,
        string Message,
        int IdUsuario,
        int IdDireccion,
        decimal Subtotal,
        decimal Descuento,
        decimal CostoEnvio,
        decimal Total
    );
}