

namespace SellPoint.Domainn.Base
{
    public abstract class Usuario : AuditEntity
    {
     public string? Nombre {  get; set; }
     public string? Apellido {  get; set; }
     public string? FullName => $"{Nombre} {Apellido}";
     public string? Email {  get; set; }
     public string Telefono {  get; set; }
     public DateTime FechaDeNacimiento { get; set; }
    }
}
