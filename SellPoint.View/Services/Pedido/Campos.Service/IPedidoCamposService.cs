using SellPoint.View.Models.Pedido;

namespace SellPoint.View.Services.Pedido.Campos.Service
{
    public interface IPedidoCamposService
    {
        PedidoCamposResult TryParseCampos(string idUsuario, string idDireccion, string subtotal, string descuento, string costoEnvio, string total);
    }
}