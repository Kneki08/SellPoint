using SellPoint.View.Models.ModelDetallePedido.Dtos;
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

        public static ApiResponse<T> CreateSuccess(T dato, string message = null)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Data = dato,
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

   
}
