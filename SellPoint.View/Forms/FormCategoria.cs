using System.Runtime.Versioning; 
using SellPoint.Aplication.Dtos.Categoria;
using SellPoint.View.Models.ModelsCategoria;
using SellPoint.View.Services.CategoriaApiClient;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SellPoint.View
{
    [SupportedOSPlatform("windows")]
    public partial class FormCategoria : Form
    {
        private readonly ICategoriaApiClient _categoriaApiClient;
        private readonly ErrorProvider _errorProvider = new ErrorProvider();

        public FormCategoria(ICategoriaApiClient categoriaApiClient)
        {
            InitializeComponent();
            _categoriaApiClient = categoriaApiClient;
        }

        private async void FormCategoria_Load(object sender, EventArgs e) => await CargarCategoriasAsync();

        private async void btnCargar_Click(object sender, EventArgs e) => await CargarCategoriasAsync();

        private async void btnCrear_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario()) return;

            var dto = new SaveCategoriaDTO
            {
                Nombre = txtNombre.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim()
            };

            await ProcesarResultadoAsync(await _categoriaApiClient.CrearAsync(dto), "creada");
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

            await ProcesarResultadoAsync(await _categoriaApiClient.ActualizarAsync(dto), "actualizada");
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("ID inválido.");
                return;
            }

            await ProcesarResultadoAsync(await _categoriaApiClient.EliminarAsync(new RemoveCategoriaDTO { Id = id }), "eliminada");
        }

        private async Task CargarCategoriasAsync()
        {
            try
            {
                var categorias = await _categoriaApiClient.ObtenerTodosAsync();
                dgvCategorias.DataSource = categorias.Select(dto => new CategoriaModel
                {
                    Id = dto.Id,
                    Nombre = dto.Nombre,
                    Descripcion = dto.Descripcion,
                    Activo = dto.Activo
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar categorías: " + ex.Message);
            }
        }

        private async Task ProcesarResultadoAsync(bool resultado, string accion)
        {
            if (resultado)
            {
                MessageBox.Show($"Categoría {accion} con éxito.");
                await CargarCategoriasAsync();
                LimpiarFormulario();
            }
            else
            {
                MessageBox.Show($"Error al {accion} la categoría.");
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
