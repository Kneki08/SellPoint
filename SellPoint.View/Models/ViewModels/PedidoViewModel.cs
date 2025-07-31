namespace SellPoint.View.Models.ViewModels
{
    public class PedidoViewModel
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdDireccionEnvio { get; set; }
        public string MetodoPago { get; set; } = string.Empty;
        public string ReferenciaPago { get; set; } = string.Empty;
        public decimal Subtotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal CostoEnvio { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string Notas { get; set; } = string.Empty;
        public DateTime FechaPedido { get; set; }
        public int? CuponId { get; set; }
    }
}