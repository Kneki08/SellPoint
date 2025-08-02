using SellPoint.Aplication.Dtos.Cupon;
using System.Windows.Forms;

namespace SellPoint.View.Helpers
{
    public static class CuponFormHelper
    {
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

        public static SaveCuponDTO ConstruirSaveDTO(TextBox txtCodigo, TextBox txtDescuento, DateTimePicker dtpFechaVencimiento)
        {
            return new SaveCuponDTO
            {
                Codigo = txtCodigo.Text.Trim(),
                ValorDescuento = decimal.Parse(txtDescuento.Text),
                FechaVencimiento = dtpFechaVencimiento.Value
            };
        }

        public static UpdateCuponDTO? ConstruirUpdateDTO(TextBox txtId, TextBox txtCodigo, TextBox txtDescuento, DateTimePicker dtpFechaVencimiento)
        {
            if (!int.TryParse(txtId.Text, out int id)) return null;

            return new UpdateCuponDTO
            {
                Id = id,
                Codigo = txtCodigo.Text.Trim(),
                ValorDescuento = decimal.Parse(txtDescuento.Text),
                FechaVencimiento = dtpFechaVencimiento.Value
            };
        }

        public static RemoveCuponDTIO? ConstruirRemoveDTO(TextBox txtId)
        {
            if (!int.TryParse(txtId.Text, out int id)) return null;

            return new RemoveCuponDTIO
            {
                Id = id
            };
        }
    }
}
