using SellPoint.View.Models.ModelsCupon;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace SellPoint.View.Helpers
{
    public static class CuponFormHelper
    {
        [SupportedOSPlatform("windows")]
        public static bool ValidarFormulario(TextBox txtCodigo, TextBox txtDescuento, ErrorProvider errorProvider)
        {
            errorProvider.Clear();
            bool valido = true;

            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                errorProvider.SetError(txtCodigo, "El código es obligatorio.");
                valido = false;
            }

            if (string.IsNullOrWhiteSpace(txtDescuento.Text) || !decimal.TryParse(txtDescuento.Text, out _))
            {
                errorProvider.SetError(txtDescuento, "El descuento debe ser numérico.");
                valido = false;
            }

            return valido;
        }
        [SupportedOSPlatform("windows")]

        public static SaveCuponModel ConstruirSaveModel(TextBox txtCodigo, TextBox txtDescuento, DateTimePicker dtpFechaVencimiento)
        {
            return new SaveCuponModel
            {
                Codigo = txtCodigo.Text.Trim(),
                ValorDescuento = decimal.Parse(txtDescuento.Text),
                FechaVencimiento = dtpFechaVencimiento.Value
            };
        }

        [SupportedOSPlatform("windows")]
        public static UpdateCuponModel? ConstruirUpdateModel(TextBox txtId, TextBox txtCodigo, TextBox txtDescuento, DateTimePicker dtpFechaVencimiento)
        {
            if (!int.TryParse(txtId.Text, out int id)) return null;

            return new UpdateCuponModel
            {
                Id = id,
                Codigo = txtCodigo.Text.Trim(),
                ValorDescuento = decimal.Parse(txtDescuento.Text),
                FechaVencimiento = dtpFechaVencimiento.Value
            };
        }

        [SupportedOSPlatform("windows")]
        public static RemoveCuponModel? ConstruirRemoveModel(TextBox txtId)
        {
            if (!int.TryParse(txtId.Text, out int id)) return null;

            return new RemoveCuponModel
            {
                Id = id
            };
        }
    }
}

