

using SellPoint.Domainn.Base;

namespace SellPoint.Domainn.Entities.Products
{
    public sealed class Cupon : AuditiEntity
    {

        public string Codigo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }

        public TipoDescuento TipoDescuento { get; set; }
        public decimal ValorDescuento { get; set; }
        public decimal MontoMinimo { get; set; } = 0m;

        public DateTime FechaInicio { get; set; }
        public DateTime FechaVencimiento { get; set; }

        public int? UsosMaximos { get; set; }
        public int UsosActuales { get; set; } = 0;

        public bool Activo { get; set; } = true;

    }

    public enum TipoDescuento
    {
        Porcentaje,
        MontoFijo
    }
}
