using SellPoint.View.Models.ModelsProducto;


namespace SellPoint.View
{
    public partial class FormProducto : Form
    {
        private readonly IProductoService _productoService;
        private readonly ErrorProvider _errorProvider = new ErrorProvider();

        public FormProducto(IProductoService productoService)
        {
            InitializeComponent();
            _productoService = productoService;
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
            var producto = ObtenerDesdeFormulario();

            if (!_productoService.ValidarFormulario(producto, out string mensaje))
            {
                MessageBox.Show(mensaje);
                return;
            }

            bool creado = await _productoService.AgregarAsync(producto);
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
            var producto = ObtenerDesdeFormulario();

            if (!_productoService.ValidarFormulario(producto, out string mensaje))
            {
                MessageBox.Show(mensaje);
                return;
            }

            bool actualizado = await _productoService.ActualizarAsync(producto);
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

            bool eliminado = await _productoService.EliminarAsync(id);
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
            var productos = await _productoService.ObtenerTodosAsync();
            dgvProductos.DataSource = productos;
        }

        private ProductoModel ObtenerDesdeFormulario()
        {
            return new ProductoModel
            {
                Id = string.IsNullOrWhiteSpace(txtId.Text) ? 0 : Convert.ToInt32(txtId.Text),
                Nombre = txtNombre.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim(),
                Precio = decimal.TryParse(txtPrecio.Text, out var precio) ? precio : 0,
                Stock = int.TryParse(txtStock.Text, out var stock) ? stock : 0,
                Activo = true
            };
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

