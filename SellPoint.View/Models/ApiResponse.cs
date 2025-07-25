namespace SellPoint.View.Models
{
    /// <summary>
    /// Representa la estructura estándar de respuesta de la API.
    /// </summary>
    /// <typeparam name="T">Tipo de dato contenido en la propiedad Data.</typeparam>
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
    }
}