using SellPoint.View.Models.Pedido;

namespace SellPoint.View.Validations
{
    public interface IPedidoValidator
    {
        (bool valido, string mensaje) ValidarGuardar(SavePedidoDTO dto);
        (bool valido, string mensaje) ValidarActualizar(UpdatePedidoDTO dto);
        (bool valido, string mensaje) ValidarEliminar(int id);
        (bool valido, string mensaje) ValidarId(int id);
    }
}