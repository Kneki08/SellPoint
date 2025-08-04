using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SellPoint.View.Models.ModelDetallePedido.Dtos;

namespace SellPoint.View.Validations
{
    public static class DetallePedidoValidator
    {
        public static bool Validate(SaveDto dto, out List<string> errors)
        {
            errors = new List<string>();

            if (dto.PedidoId <= 0) errors.Add("El ID de pedido debe ser mayor a 0");
            if (dto.ProductoId <= 0) errors.Add("El ID de producto debe ser mayor a 0");
            if (dto.Cantidad <= 0) errors.Add("La cantidad debe ser mayor a 0");
            if (dto.PrecioUnitario <= 0) errors.Add("El precio unitario debe ser mayor a 0");

            return errors.Count == 0;
        }

        public static bool ValidateId(int id, out string error)
        {
            error = id > 0 ? null : "El ID debe ser mayor a 0";
            return error == null;
        }
    }
}
