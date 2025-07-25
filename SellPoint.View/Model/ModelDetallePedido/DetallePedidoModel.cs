using System.Text.Json.Serialization;

namespace SellPoint.View.Models.ModelDetallePedido
{
    public class DetallePedidoModel
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int PedidoId { get; set; }
        public int ProductoId { get; set; }
    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
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
