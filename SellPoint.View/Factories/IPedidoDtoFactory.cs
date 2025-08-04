using SellPoint.View.Models.Pedido;
using SellPoint.View.Validations;

namespace SellPoint.View.Factories
{
    public interface IPedidoDtoFactory
    {
        SavePedidoDTO CrearSaveDTO(PedidoCamposResult campos, string metodoPago, string referenciaPago, string estado, string notas);
        UpdatePedidoDTO CrearUpdateDTO(PedidoCamposResult campos, PedidoDTO selected, string metodoPago, string referenciaPago, string estado, string notas);
    }
}