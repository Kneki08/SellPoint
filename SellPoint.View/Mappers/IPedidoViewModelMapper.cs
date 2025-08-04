using SellPoint.View.Models.Pedido;
using SellPoint.View.Models.ViewModels;

namespace SellPoint.View.Mappers
{
    public interface IPedidoViewModelMapper
    {
        PedidoViewModel ToViewModel(PedidoDTO dto);
        SavePedidoDTO ToSaveDTO(PedidoViewModel vm);
        UpdatePedidoDTO ToUpdateDTO(PedidoViewModel vm);
    }
}