namespace SellPoint.Aplication.Dtos.Pedido
{
    public class PedidoDTO : BasePedidoDTO
    {
        public required int Id { get; set; }
        public string NumeroPedido { get; set; } = string.Empty;
        public string Notas { get; set; } = string.Empty;
    }
}