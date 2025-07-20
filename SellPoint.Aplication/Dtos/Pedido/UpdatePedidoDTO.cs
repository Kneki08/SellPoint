namespace SellPoint.Aplication.Dtos.Pedido
{
    public class UpdatePedidoDTO : BasePedidoDTO
    {
        public required int Id { get; set; }
        public string Notas { get; set; } = string.Empty;

        public required DateTime FechaActualizacion { get; set; } // Agregado para corrección
    }
}