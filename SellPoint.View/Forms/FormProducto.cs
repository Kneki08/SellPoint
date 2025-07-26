using SellPoint.Aplication.Dtos.Producto;
using SellPoint.Aplication.Dtos.ProductoDTO;
using SellPoint.View.Models.ModelsProducto;
using SellPoint.View.Service.ServiceProducto;


namespace SellPoint.View
{
    public partial class FormProducto : Form
    {
        private readonly IProductoApiClient _productoApiClient;
        private readonly ErrorProvider _errorProvider = new ErrorProvider();

        public FormProducto(IProductoApiClient productoApiClient)
        {
            InitializeComponent();
            _productoApiClient = productoApiClient;
        }

        private async void FormProducto_Load(object sender, EventArgs e)
        {
            await CargarProductosAsync();
        }

        private async void btnCargar_Click(object sender, EventArgs e)
        {
            await CargarProductosAsync();
        }

        private async void btnCrear_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario()) return;

            var dto = new SaveProductoDTO
            {
                Nombre = txtNombre.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim(),
                Precio = decimal.Parse(txtPrecio.Text),
                Stock = int.Parse(txtStock.Text)
            };

            bool creado = await _productoApiClient.CrearAsync(dto);
            if (creado)
            {
                MessageBox.Show("Producto creado con éxito.");
                await CargarProductosAsync();
                LimpiarFormulario();
            }
            else
            {
                MessageBox.Show("Error al crear el producto.");
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

            var dto = new UpdateProductoDTO
            {
                Id = id,
                Nombre = txtNombre.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim(),
                Precio = decimal.Parse(txtPrecio.Text),
                Stock = int.Parse(txtStock.Text),
                Activo = true
            };

            bool actualizado = await _productoApiClient.ActualizarAsync(dto);
            if (actualizado)
            {
                MessageBox.Show("Producto actualizado con éxito.");
                await CargarProductosAsync();
                LimpiarFormulario();
            }
            else
            {
                MessageBox.Show("Error al actualizar el producto.");
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("ID inválido.");
                return;
            }

            var dto = new RemoveProductoDTO { Id = id };
            bool eliminado = await _productoApiClient.EliminarAsync(dto);
            if (eliminado)
            {
                MessageBox.Show("Producto eliminado con éxito.");
                await CargarProductosAsync();
                LimpiarFormulario();
            }
            else
            {
                MessageBox.Show("Error al eliminar el producto.");
            }
        }

        private async Task CargarProductosAsync()
        {
            try
            {
                var productosDTO = await _productoApiClient.ObtenerTodosAsync();
                var productosModel = productosDTO.Select(dto => new ProductoModel
                {
                    Id = dto.Id,
                    Nombre = dto.Nombre,
                    Descripcion = dto.Descripcion,
                    Precio = dto.Precio,
                    Stock = dto.Stock,
                    Activo = dto.Activo
                }).ToList();

                dgvProductos.DataSource = productosModel;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message);
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

            if (!decimal.TryParse(txtPrecio.Text, out _))
            {
                _errorProvider.SetError(txtPrecio, "El precio debe ser válido.");
                valido = false;
            }

            if (!int.TryParse(txtStock.Text, out _))
            {
                _errorProvider.SetError(txtStock, "El stock debe ser numérico.");
                valido = false;
            }

            return valido;
        }

        private void LimpiarFormulario()
        {
            txtId.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
            txtStock.Text = "";
            _errorProvider.Clear();
        }
    }
}
