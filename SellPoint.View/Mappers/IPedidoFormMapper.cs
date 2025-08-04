using SellPoint.View.Models.ViewModels;
using SellPoint.View.Forms;

namespace SellPoint.View.Mappers
{
    public interface IPedidoFormMapper
    {
        PedidoViewModel FormToViewModel(PedidoForm form);
        void ViewModelToForm(PedidoViewModel vm, PedidoForm form);
    }
}