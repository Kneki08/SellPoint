namespace SellPoint.View.Models.Pedido
{
    /// <summary>
    /// DTO para actualizar un pedido existente.
    /// </summary>
    public class UpdatePedidoDTO : BasePedidoDTO
    {
        public required int Id { get; set; }
        public string Notas { get; set; } = string.Empty;
        public required DateTime FechaActualizacion { get; set; }
    }
}