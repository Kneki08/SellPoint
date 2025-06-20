
namespace SellPoint.Domain.Base
{

    public class OperationResult
    {
        public bool IsSucess { get; set; }
        public string Message { get; set; } = string.Empty;
        public dynamic? Data { get; set; }

        public OperationResult()
        {
            this.IsSucess = true;
        }

    }
}
