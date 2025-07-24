
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
            var dto = new SaveCategoriaDTO
            {
                Nombre = txtNombre.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim()
            };

            bool creado = await _categoriaApiClient.CrearAsync(dto);
            if (creado)
            {
                MessageBox.Show("Categor�a creada con �xito.");
                await CargarCategoriasAsync();
            }
            else
            {
                MessageBox.Show("Error al crear la categor�a.");
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
                Nombre = txtNombre.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim()
            };

            bool actualizado = await _categoriaApiClient.ActualizarAsync(dto);
            if (actualizado)
            {
                MessageBox.Show("Categor�a actualizada con �xito.");
                await CargarCategoriasAsync();
            }
            else
            {
                MessageBox.Show("Error al actualizar la categor�a.");
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

            bool eliminado = await _categoriaApiClient.EliminarAsync(dto);
            if (eliminado)
            {
                MessageBox.Show("Categor�a eliminada con �xito.");
                await CargarCategoriasAsync();
            }
            else
            {
                MessageBox.Show("Error al eliminar la categor�a.");
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
                MessageBox.Show("Error al cargar categor�as: " + ex.Message);
            }
        }
    }
}

