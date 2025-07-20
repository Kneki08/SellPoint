using SellPoint.Aplication.Dtos.Pedido;

public class PedidoDTO : BasePedidoDTO
{
    public required int Id { get; set; }
    public string NumeroPedido { get; set; } = string.Empty;

    public decimal Subtotal { get; set; }
    public decimal Descuento { get; set; }
    public decimal CostoEnvio { get; set; }
    public decimal Total { get; set; }

    public string Notas { get; set; } = string.Empty;
}