

using SellPoint.Domainn.Base;

namespace SellPoint.Domainn.Entities.Users
{
    public sealed class Cliente : Usuario
    {
        public string? DireccionEnvio { get; set; }
        public decimal SaldoCredito { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    }
}
