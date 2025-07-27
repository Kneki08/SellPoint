using SellPoint.View.Forms;
using SellPoint.View.Models.Pedido;
public static class PedidoUiMapper
{
    public static void CargarEnCampos(PedidoDTO dto, PedidoForm form)
    {
        form.txtIdUsuario.Text = dto.IdUsuario.ToString();
        form.txtDireccion.Text = dto.IdDireccionEnvio.ToString();
        form.txtMetodoPago.Text = dto.MetodoPago;
        form.txtReferencia.Text = dto.ReferenciaPago;
        form.txtSubtotal.Text = dto.Subtotal.ToString();
        form.txtDescuento.Text = dto.Descuento.ToString();
        form.txtCostoEnvio.Text = dto.CostoEnvio.ToString();
        form.txtTotal.Text = dto.Total.ToString();
        form.cmbEstado.SelectedItem = dto.Estado;
        form.txtNotas.Text = dto.Notas;
    }
}