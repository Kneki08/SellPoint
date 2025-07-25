namespace SellPoint.View.Dtos.Pedido
{
    /// <summary>
    /// DTO para guardar un nuevo pedido.
    /// </summary>
    public class SavePedidoDTO : BasePedidoDTO
    {
        public string Notas { get; set; } = string.Empty;
    }
}