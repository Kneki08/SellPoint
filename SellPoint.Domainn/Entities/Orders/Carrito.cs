
using SellPoint.Domainn.Base;

namespace SellPoint.Domain.Entities.Orders
{
    public sealed class Carrito : AuditEntity
    {

        public int UsuarioId { get; set; }           
        public int ProductoId { get; set; }          
        public int Cantidad { get; set; }
        public DateTime FechaAgregado { get; set; }
        public DateTime FechaActualizacion { get; set; }

        public bool EsCantidadValida() => Cantidad > 0;


    }
}
