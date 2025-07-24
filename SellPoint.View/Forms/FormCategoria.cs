using SellPoint.View.Models.ModelsCategoria;
using SellPoint.View.Services.CategoriaApiClient;

namespace SellPoint.View
{
    public partial class FormCategoria : Form
    {
        private readonly CategoriaApiClient _categoriaService;

        public FormCategoria(CategoriaApiClient categoriaService)
        {
            InitializeComponent();
            _categoriaService = new CategoriaApiClient(new HttpClient());
            _categoriaService = categoriaService;
        }

        private async void FormCategoria_Load(object sender, EventArgs e)
        {
            await CargarCategoriasAsync();
        }

        private async void btnCargar_Click(object sender, EventArgs e)
        {
            await CargarCategoriasAsync();
        }

        private async Task CargarCategoriasAsync()
        {
            try
            {
                var categorias = await _categoriaService.ObtenerTodosAsync();
                dgvCategorias.DataSource = categorias != null ? new BindingSource(categorias, null) : null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar categorías: {ex.Message}");
            }
        }

        private async void btnCrear_Click(object sender, EventArgs e)
        {
            var dto = new SaveCategoriaDTO
            {
                Nombre = txtNombre.Text,
                Descripcion = txtDescripcion.Text
            };

            try
            {
                var success = await _categoriaService.CrearAsync(dto);
                if (success)
                {
                    MessageBox.Show("Categoría creada correctamente.");
                    await CargarCategoriasAsync();
                }
                else
                {
                    MessageBox.Show("No se pudo crear la categoría.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear: {ex.Message}");
            }
        }

        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("ID inválido.");
                return;
            }

            var dto = new UpdateCategoriaDTO
            {
                Id = id,
                Nombre = txtNombre.Text,
                Descripcion = txtDescripcion.Text
            };

            try
            {
                var success = await _categoriaService.ActualizarAsync(dto);
                if (success)
                {
                    MessageBox.Show("Categoría actualizada.");
                    await CargarCategoriasAsync();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar: {ex.Message}");
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

            try
            {
                var success = await _categoriaService.EliminarAsync(dto);
                if (success)
                {
                    MessageBox.Show("Categoría eliminada.");
                    await CargarCategoriasAsync();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar: {ex.Message}");
            }
        }
    }
}
