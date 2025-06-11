

namespace SellPoint.Domainn.Base
{
    public abstract class Usuario
    {
     public string? Nombre {  get; set; }
     public string? Apellido {  get; set; }
     public string? FullName => $"{Nombre} {Apellido}";
     public string? Email {  get; set; }
     public string telefono {  get; set; }
     public DateTime FechaDeNacimiento { get; set; }
    }
}
