using SellPoint.Aplication.Dtos.DetallePedido;
using SellPoint.View.Service.DetallePedidoClient;
using SellPoint.View.Models.ModelDetallePedido;
using System.Windows.Forms;

namespace SellPoint.View
{
    public partial class Form1 : Form
    {
        private readonly DetallePedidoApiClient _detallePedidoApiClient;

        public Form1()
        {
            InitializeComponent();
            _detallePedidoApiClient = new DetallePedidoApiClient();
        }
        private async void CargarDatos()
        {
            try
            {
                var lista = await _detallePedidoApiClient.ObtenerTodosAsync();

                // Validar si hay datos
                if (lista == null || lista.Count == 0)
                {
                    MessageBox.Show("No se encontraron registros.");
                    dgvDetallePedido.DataSource = null;
                    return;
                }

                // Mostrar cuántos registros se obtuvieron
                MessageBox.Show($"Registros obtenidos: {lista.Count}");

                // Asignar la lista al DataGridView
                dgvDetallePedido.DataSource = lista;

                // Configurar encabezados
                dgvDetallePedido.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvDetallePedido.Columns["Id"].HeaderText = "ID";
                dgvDetallePedido.Columns["Cantidad"].HeaderText = "Cantidad";
                dgvDetallePedido.Columns["PrecioUnitario"].HeaderText = "Precio Unitario";
                dgvDetallePedido.Columns["PedidoId"].HeaderText = "ID Pedido";
                dgvDetallePedido.Columns["ProductoId"].HeaderText = "ID Producto";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos: {ex.Message}");
            }
        }
        private async void btnCargar_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private async void btnCrear_Click(object sender, EventArgs e)
        {
            var dto = new SaveDetallePedidoDTO
            {
                Cantidad = int.Parse(txtCantidad.Text),
                PrecioUnitario = decimal.Parse(txtPrecio.Text),
                PedidoId = int.Parse(txtPedidoId.Text),
                ProductoId = int.Parse(txtProductoId.Text)
            };

            var resultado = await _detallePedidoApiClient.CrearAsync(dto);
            if (resultado)
            {
                MessageBox.Show("Detalle creado correctamente.");
                btnCargar.PerformClick();
            }
            else
            {
                MessageBox.Show("Error al crear detalle.");
            }
        }

        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            var dto = new UpdateDetallePedidoDTO
            {
                Id = int.Parse(txtId.Text),
                Cantidad = int.Parse(txtCantidad.Text),
                PrecioUnitario = decimal.Parse(txtPrecio.Text),
                PedidoId = int.Parse(txtPedidoId.Text),
                ProductoId = int.Parse(txtProductoId.Text)
            };

            var resultado = await _detallePedidoApiClient.ActualizarAsync(dto);
            if (resultado)
            {
                MessageBox.Show("Detalle actualizado correctamente.");
                btnCargar.PerformClick();
            }
            else
            {
                MessageBox.Show("Error al actualizar detalle.");
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            var dto = new RemoveDetallePedidoDTO
            {
                Id = int.Parse(txtId.Text)
            };

            var resultado = await _detallePedidoApiClient.EliminarAsync(dto);
            if (resultado)
            {
                MessageBox.Show("Detalle eliminado correctamente.");
                btnCargar.PerformClick();
            }
            else
            {
                MessageBox.Show("Error al eliminar detalle.");
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
