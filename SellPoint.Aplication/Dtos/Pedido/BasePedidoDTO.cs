namespace SellPoint.Aplication.Dtos.Pedido
{
    public abstract class BasePedidoDTO
    {
        public required int IdUsuario { get; set; }
        public required DateTime FechaPedido { get; set; }
        public required string Estado { get; set; } 
        public required int IdDireccionEnvio { get; set; }

        public required int? CuponId { get; set; } 

        public required string MetodoPago { get; set; }
        public required string ReferenciaPago { get; set; }
    }
}