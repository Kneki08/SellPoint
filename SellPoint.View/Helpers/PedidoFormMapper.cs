using System;
using SellPoint.View.Forms;
using SellPoint.View.Models.ViewModels;

namespace SellPoint.View.Helpers
{
    public static class PedidoFormMapper
    {
        // 1. Pasa los datos del formulario al ViewModel (para agregar/actualizar)
        public static PedidoViewModel FormToViewModel(PedidoForm form)
        {
            return new PedidoViewModel
            {
                Id = 0, 
                IdUsuario = Convert.ToInt32(form.cmbUsuarios.SelectedItem ?? "0"),
                IdDireccionEnvio = Convert.ToInt32(form.cmbDirecciones.SelectedItem ?? "0"),
                MetodoPago = form.cmbMetodoPago.SelectedItem?.ToString() ?? "PayPal",
                ReferenciaPago = form.txtReferencia.Text ?? "",
                Subtotal = decimal.TryParse(form.txtSubtotal.Text, out var s) ? s : 0,
                Descuento = decimal.TryParse(form.txtDescuento.Text, out var d) ? d : 0,
                CostoEnvio = decimal.TryParse(form.txtCostoEnvio.Text, out var c) ? c : 0,
                Total = decimal.TryParse(form.txtTotal.Text, out var t) ? t : 0,
                Estado = form.cmbEstado.SelectedItem?.ToString() ?? "EnPreparacion",
                Notas = form.txtNotas.Text ?? "",

            };
        }

        // 2. Carga los datos de un ViewModel en los controles del formulario (en el grid)
        public static void ViewModelToForm(PedidoViewModel vm, PedidoForm form)
        {
            form.cmbUsuarios.SelectedItem = vm.IdUsuario;
            form.cmbDirecciones.SelectedItem = vm.IdDireccionEnvio;
            form.cmbMetodoPago.SelectedItem = vm.MetodoPago;
            form.txtReferencia.Text = vm.ReferenciaPago;
            form.txtSubtotal.Text = vm.Subtotal.ToString("0.##");
            form.txtDescuento.Text = vm.Descuento.ToString("0.##");
            form.txtCostoEnvio.Text = vm.CostoEnvio.ToString("0.##");
            form.txtTotal.Text = vm.Total.ToString("0.##");
            form.cmbEstado.SelectedItem = vm.Estado;
            form.txtNotas.Text = vm.Notas ?? "";
        }
    }
}