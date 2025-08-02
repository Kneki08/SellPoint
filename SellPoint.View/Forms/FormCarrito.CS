using SellPoint.View.Models.ModelsCarito;
using SellPoint.View.Service.ServiceCarrito;

namespace SellPoint.View.Forms
{
    public partial class FormCarrito : Form
    {
        private readonly ICarritoService _carritoService;

        public FormCarrito(ICarritoService carritoService)
        {
            InitializeComponent();
            _carritoService = carritoService;
        }

        private async void FormCarrito_Load(object sender, EventArgs e)
        {
            await CargarCarritosAsync();
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            var carrito = ObtenerDesdeFormulario();

            if (!_carritoService.ValidarFormulario(carrito, out string mensaje))
            {
                MessageBox.Show(mensaje, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var exito = await _carritoService.AgregarAsync(carrito);

            if (exito)
            {
                MessageBox.Show("Carrito agregado correctamente.");
                await CargarCarritosAsync();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Error al agregar carrito.");
            }
        }

        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            var carrito = ObtenerDesdeFormulario();

            if (!_carritoService.ValidarFormulario(carrito, out string mensaje))
            {
                MessageBox.Show(mensaje, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var exito = await _carritoService.ActualizarAsync(carrito);

            if (exito)
            {
                MessageBox.Show("Carrito actualizado correctamente.");
                await CargarCarritosAsync();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Error al actualizar carrito.");
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("ID no válido para eliminar.");
                return;
            }

            var exito = await _carritoService.EliminarAsync(id);

            if (exito)
            {
                MessageBox.Show("Carrito eliminado correctamente.");
                await CargarCarritosAsync();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Error al eliminar carrito.");
            }
        }

        private async void btnCargar_Click(object sender, EventArgs e)
        {
            await CargarCarritosAsync();
        }

        private async Task CargarCarritosAsync()
        {
            var lista = await _carritoService.ObtenerTodosAsync();
            dgvCarrito.DataSource = lista;
        }

        private CarritoModel ObtenerDesdeFormulario()
        {
            return new CarritoModel
            {
                Id = string.IsNullOrWhiteSpace(txtId.Text) ? 0 : Convert.ToInt32(txtId.Text),
                ClienteId = Convert.ToInt32(txtIdCliente.Text),
                IdProducto = Convert.ToInt32(txtIdProducto.Text),
                Cantidad = Convert.ToInt32(txtCantidad.Text)
            };
        }

        private void LimpiarCampos()
        {
            txtId.Text = "";
            txtIdCliente.Text = "";
            txtIdProducto.Text = "";
            txtCantidad.Text = "";
        }
    }
}

