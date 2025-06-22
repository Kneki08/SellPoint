

namespace SellPoint.Aplication.Dtos.Cupon
{
    public record SaveCuponDTO
    {
        public string Codigo { get; init; }
        public string Descripcion { get; init; }
        public string TipoDescuento { get; init; } 
        public decimal ValorDescuento { get; init; }
        public decimal MontoMinimo { get; init; }
        public DateTime FechaInicio { get; init; }
        public DateTime FechaVencimiento { get; init; }
        public int? UsosMaximos { get; init; }
        public bool Activo { get; init; }
    }
}
