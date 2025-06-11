

namespace SellPoint.Domainn.Base
{
    public abstract class AuditiEntity
    {
      public DateTime fecha_creacion {  get; set; } = DateTime.UtcNow;
        public string? usuario_creo { get; set; }
        
     public DateTime? fecha_actualizacion { get; set; }
        public string? usuario_actualizacion { get; set; }
     public DateTime? fecha_eliminacion { get; set; }
        public string? usuario_eliminacion { get; set; }

        public bool esta_eliminado { get; set; } = false;
    }
}
