
using SellPoint.View.Models.ModelsCategoria;
using SellPoint.View.Services.CategoriaApiClient;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SellPoint.View
{
    public partial class FormCategoria : Form
    {
        private readonly ICategoriaApiClient _categoriaApiClient;
        private readonly ErrorProvider _errorProvider = new ErrorProvider();

        public FormCategoria(ICategoriaApiClient categoriaApiClient)
        {
            InitializeComponent();
            _categoriaApiClient = categoriaApiClient;
        }

        private async void FormCategoria_Load(object sender, EventArgs e)
        {
            await CargarCategoriasAsync();
        }

        private async void btnCargar_Click(object sender, EventArgs e)
        {
            await CargarCategoriasAsync();
        }

        private async void btnCrear_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario()) return;

            var dto = new SaveCategoriaDTO
            {
                Nombre = txtNombre.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim()
            };

            bool creado = await _categoriaApiClient.CrearAsync(dto);
            if (creado)
            {
                MessageBox.Show("Categoría creada con éxito.");
                await CargarCategoriasAsync();
                LimpiarFormulario();
            }
            else
            {
                MessageBox.Show("Error al crear la categoría.");
            }
        }

        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("ID inválido.");
                return;
            }

            if (!ValidarFormulario()) return;

            var dto = new UpdateCategoriaDTO
            {
                Id = id,
                Nombre = txtNombre.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim()
            };

            bool actualizado = await _categoriaApiClient.ActualizarAsync(dto);
            if (actualizado)
            {
                MessageBox.Show("Categoría actualizada con éxito.");
                await CargarCategoriasAsync();
                LimpiarFormulario();
            }
            else
            {
                MessageBox.Show("Error al actualizar la categoría.");
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("ID inválido.");
                return;
            }

            var dto = new RemoveCategoriaDTO { Id = id };
            bool eliminado = await _categoriaApiClient.EliminarAsync(dto);
            if (eliminado)
            {
                MessageBox.Show("Categoría eliminada con éxito.");
                await CargarCategoriasAsync();
                LimpiarFormulario();
            }
            else
            {
                MessageBox.Show("Error al eliminar la categoría.");
            }
        }

        private async Task CargarCategoriasAsync()
        {
            try
            {
                var categorias = await _categoriaApiClient.ObtenerTodosAsync();
                dgvCategorias.DataSource = categorias.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar categorías: " + ex.Message);
            }
        }

        private bool ValidarFormulario()
        {
            _errorProvider.Clear();
            bool valido = true;

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                _errorProvider.SetError(txtNombre, "El nombre es obligatorio.");
                valido = false;
            }

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                _errorProvider.SetError(txtDescripcion, "La descripción es obligatoria.");
                valido = false;
            }

            return valido;
        }

        private void LimpiarFormulario()
        {
            txtId.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            _errorProvider.Clear();
        }
    }
}
