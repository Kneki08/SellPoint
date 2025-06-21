
namespace SellPoint.Domain.Base
{

    public class OperationResult<T> where T : class
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        // Constructores estáticos para claridad al retornar
        public static OperationResult<T> Success(T? data = null, string message = "Operación exitosa.")
        {
            return new OperationResult<T>
            {
                IsSuccess = true,
                Message = message,
                Data = data
            };
        }
        public static OperationResult<T> Failure(string message = "Ocurrió un error.")
        {
            return new OperationResult<T>
            {
                IsSuccess = false,
                Message = message
            };
        }
    }
}
