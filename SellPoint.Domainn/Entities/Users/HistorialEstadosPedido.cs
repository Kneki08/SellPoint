
using SellPoint.Domainn.Base;
using SellPoint.Domainn.Entities.Orders;

namespace SellPoint.Domain.Entities.Users
{
    public sealed class HistorialEstadosPedido : AuditEntity
    {
        public int PedidoId { get; set; }
        public string? EstadoAnterior { get; set; }
        public string EstadoNuevo { get; set; } = null!;
        public string? Comentario { get; set; }
        public int? UsuarioAdminId { get; set; }

        public Pedido? Pedido { get; set; }
        public UsuarioBase? UsuarioAdmin { get; set; }

    }
}
