using SellPoint.View.Models.ModelsCategoria;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace SellPoint.View.Helpers
{
    public static class CategoriaFormHelper
    {
        [SupportedOSPlatform("windows")]
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
        
        [SupportedOSPlatform("windows")]
        public static SaveCategoriaModel ConstruirSaveModel(TextBox txtNombre, TextBox txtDescripcion, CheckBox chkActivo, CheckBox chkEliminado)
        {
            return new SaveCategoriaModel
            {
                Nombre = txtNombre.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim(),
                Activo = chkActivo.Checked,
                EstaEliminado = chkEliminado.Checked
            };
        }

        [SupportedOSPlatform("windows")]
        public static UpdateCategoriaModel? ConstruirUpdateModel(TextBox txtId, TextBox txtNombre, TextBox txtDescripcion, CheckBox chkActivo, CheckBox chkEliminado)
        {
            if (!int.TryParse(txtId.Text, out int id)) return null;

            return new UpdateCategoriaModel
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

