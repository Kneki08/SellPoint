using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.View.Models.ModelDetallePedido
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        public static ApiResponse<T> CreateSuccess(T data, string message = null)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Data = data,
                Message = message ?? "Operación exitosa"
            };
        }

        public static ApiResponse<T> CreateError(string errorMessage, List<string> errors = null)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = errorMessage,
                Errors = errors ?? new List<string>()
            };
        }
    }

    // Versión no genérica para operaciones sin retorno
    public class DetallePedidoModelResponse
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public List<DetallePedidoModel> data { get; set; }
    }

    public class DetallePedidoModelResponseSingle
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public DetallePedidoModel data { get; set; }

    }
}
