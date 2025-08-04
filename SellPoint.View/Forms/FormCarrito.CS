using SellPoint.View.Models.ModelsCarito;
using SellPoint.View.Models.ModelsCarrito;
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

            if (!_carritoService.Validar(carrito, out string mensaje))
            {
                MostrarMensaje(mensaje, "Advertencia");
                return;
            }

            if (await _carritoService.AgregarAsync(carrito))
            {
                MostrarMensaje("Carrito agregado correctamente.");
                await RefrescarFormularioAsync();
            }
            else
            {
                MostrarMensaje("Error al agregar carrito.", "Error");
            }
        }

        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            var carrito = ObtenerDesdeFormulario();

            if (!_carritoService.Validar(carrito, out string mensaje))
            {
                MostrarMensaje(mensaje, "Advertencia");
                return;
            }

            if (await _carritoService.ActualizarAsync(carrito))
            {
                MostrarMensaje("Carrito actualizado correctamente.");
                await RefrescarFormularioAsync();
            }
            else
            {
                MostrarMensaje("Error al actualizar carrito.", "Error");
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MostrarMensaje("ID no válido para eliminar.", "Advertencia");
                return;
            }

            if (await _carritoService.EliminarAsync(id))
            {
                MostrarMensaje("Carrito eliminado correctamente.");
                await RefrescarFormularioAsync();
            }
            else
            {
                MostrarMensaje("Error al eliminar carrito.", "Error");
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

        private async Task RefrescarFormularioAsync()
        {
            await CargarCarritosAsync();
            LimpiarCampos();
        }

        private CarritoModel ObtenerDesdeFormulario()
        {
            int.TryParse(txtId.Text, out int id);
            int.TryParse(txtIdCliente.Text, out int usuarioId);
            int.TryParse(txtIdProducto.Text, out int productoId);
            int.TryParse(txtCantidad.Text, out int cantidad);

            return new CarritoModel
            {
                Id = id,
                UsuarioId = usuarioId,
                ProductoId = productoId,
                Cantidad = cantidad
            };
        }

        private void LimpiarCampos()
        {
            txtId.Clear();
            txtIdCliente.Clear();
            txtIdProducto.Clear();
            txtCantidad.Clear();
        }

        private void MostrarMensaje(string mensaje, string titulo = "Información")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}


