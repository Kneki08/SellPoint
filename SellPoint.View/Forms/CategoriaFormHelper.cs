using SellPoint.Aplication.Dtos.Categoria;
using System.Windows.Forms;

namespace SellPoint.View.Helpers
{
    public static class CategoriaFormHelper
    {
        public static bool ValidarFormulario(TextBox txtNombre, TextBox txtDescripcion, ErrorProvider errorProvider)
        {
            bool valido = true;
            errorProvider.Clear();

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                errorProvider.SetError(txtNombre, "El nombre es obligatorio.");
                valido = false;
            }

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                errorProvider.SetError(txtDescripcion, "La descripción es obligatoria.");
                valido = false;
            }

            return valido;
        }

        public static SaveCategoriaDTO ConstruirSaveDTO(TextBox txtNombre, TextBox txtDescripcion, CheckBox chkActivo, CheckBox chkEliminado)
        {
            return new SaveCategoriaDTO
            {
                Nombre = txtNombre.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim(),
                Activo = chkActivo.Checked,
                EstaEliminado = chkEliminado.Checked
            };
        }

        public static UpdateCategoriaDTO? ConstruirUpdateDTO(TextBox txtId, TextBox txtNombre, TextBox txtDescripcion, CheckBox chkActivo, CheckBox chkEliminado)
        {
            if (!int.TryParse(txtId.Text, out int id)) return null;

            return new UpdateCategoriaDTO
            {
                Id = id,
                Nombre = txtNombre.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim(),
                Activo = chkActivo.Checked,
                EstaEliminado = chkEliminado.Checked
            };
        }
    }
}
