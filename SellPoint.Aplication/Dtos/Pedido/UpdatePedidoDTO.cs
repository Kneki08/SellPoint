namespace SellPoint.Aplication.Dtos.Pedido
{
    public class UpdatePedidoDTO : BaseMontosPedidoDTO
    {
        public required int Id { get; set; }
        public string Notas { get; set; } = string.Empty;
        public required DateTime FechaActualizacion { get; set; }
    }
}
