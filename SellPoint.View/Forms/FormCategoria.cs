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
                MessageBox.Show($"Error al cargar categor�as: {ex.Message}");
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
                    MessageBox.Show("Categor�a creada correctamente.");
                    await CargarCategoriasAsync();
                }
                else
                {
                    MessageBox.Show("No se pudo crear la categor�a.");
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
                MessageBox.Show("ID inv�lido.");
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
                    MessageBox.Show("Categor�a actualizada.");
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
                MessageBox.Show("ID inv�lido.");
                return;
            }

            var dto = new RemoveCategoriaDTO { Id = id };

            try
            {
                var success = await _categoriaService.EliminarAsync(dto);
                if (success)
                {
                    MessageBox.Show("Categor�a eliminada.");
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
