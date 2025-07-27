namespace SellPoint.View.Models.Pedido
{
    /// <summary>
    /// DTO para visualizar un pedido obtenido desde la API.
    /// </summary>
    public class PedidoDTO : BasePedidoDTO
    {
        public required int Id { get; set; }
        public string NumeroPedido { get; set; } = string.Empty;
        public string Notas { get; set; } = string.Empty;
    }
}