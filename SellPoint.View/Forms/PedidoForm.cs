using SellPoint.View.Dtos.Pedido;
using SellPoint.View.Services.Pedido;
using System;
using System.Windows.Forms;

namespace SellPoint.View.Forms
{
    public partial class PedidoForm : Form
    {
        private readonly IPedidoService _pedidoService;

        public PedidoForm()
        {
            InitializeComponent();
            _pedidoService = new PedidoService(); // o inyectar si configuras IoC

            cmbEstado.Items.AddRange(new[] { "EnPreparacion", "Enviado", "Entregado", "Cancelado" });
            cmbEstado.SelectedIndex = 0;

            CargarPedidosAsync();
        }

        private async void CargarPedidosAsync()
        {
            var pedidos = await _pedidoService.ObtenerTodosAsync();
            dgvPedidos.DataSource = pedidos;

            // Autoajustar columnas al contenido de las celdas
            dgvPedidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvPedidos.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void LimpiarCampos()
        {
            txtIdUsuario.Text = "";
            txtDireccion.Text = "";
            txtMetodoPago.Text = "";
            txtReferencia.Text = "";
            txtSubtotal.Text = "";
            txtDescuento.Text = "";
            txtCostoEnvio.Text = "";
            txtTotal.Text = "";
            txtNotas.Text = "";
            cmbEstado.SelectedIndex = 0;
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            var dto = new SavePedidoDTO
            {
                IdUsuario = int.Parse(txtIdUsuario.Text),
                IdDireccionEnvio = int.Parse(txtDireccion.Text),
                MetodoPago = txtMetodoPago.Text,
                ReferenciaPago = txtReferencia.Text,
                Subtotal = decimal.Parse(txtSubtotal.Text),
                Descuento = decimal.Parse(txtDescuento.Text),
                CostoEnvio = decimal.Parse(txtCostoEnvio.Text),
                Total = decimal.Parse(txtTotal.Text),
                Estado = cmbEstado.SelectedItem?.ToString() ?? "EnPreparacion",
                FechaPedido = DateTime.Now,
                CuponId = null,
                Notas = txtNotas.Text
            };

            var (exito, mensaje) = await _pedidoService.AgregarAsync(dto);
            MessageBox.Show(mensaje, exito ? "Éxito" : "Error", MessageBoxButtons.OK, exito ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            if (exito)
            {
                CargarPedidosAsync();
                LimpiarCampos();
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvPedidos.CurrentRow?.DataBoundItem is not PedidoDTO pedido)
                return;

            var confirmado = MessageBox.Show("¿Seguro que deseas eliminar este pedido?", "Confirmar", MessageBoxButtons.YesNo);
            if (confirmado == DialogResult.Yes)
            {
                var (exito, mensaje) = await _pedidoService.EliminarAsync(pedido.Id);
                MessageBox.Show(mensaje, exito ? "Éxito" : "Error", MessageBoxButtons.OK, exito ? MessageBoxIcon.Information : MessageBoxIcon.Error);
                if (exito)
                    CargarPedidosAsync();
            }
        }

        private void dgvPedidos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPedidos.CurrentRow?.DataBoundItem is not PedidoDTO pedido)
                return;

            txtIdUsuario.Text = pedido.IdUsuario.ToString();
            txtDireccion.Text = pedido.IdDireccionEnvio.ToString();
            txtMetodoPago.Text = pedido.MetodoPago;
            txtReferencia.Text = pedido.ReferenciaPago;
            txtSubtotal.Text = pedido.Subtotal.ToString();
            txtDescuento.Text = pedido.Descuento.ToString();
            txtCostoEnvio.Text = pedido.CostoEnvio.ToString();
            txtTotal.Text = pedido.Total.ToString();
            cmbEstado.SelectedItem = pedido.Estado;
            txtNotas.Text = pedido.Notas;
        }

        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvPedidos.CurrentRow?.DataBoundItem is not PedidoDTO selected)
                return;

            var dto = new UpdatePedidoDTO
            {
                Id = selected.Id,
                IdUsuario = int.Parse(txtIdUsuario.Text),
                IdDireccionEnvio = int.Parse(txtDireccion.Text),
                MetodoPago = txtMetodoPago.Text,
                ReferenciaPago = txtReferencia.Text,
                Subtotal = decimal.Parse(txtSubtotal.Text),
                Descuento = decimal.Parse(txtDescuento.Text),
                CostoEnvio = decimal.Parse(txtCostoEnvio.Text),
                Total = decimal.Parse(txtTotal.Text),
                Estado = cmbEstado.SelectedItem?.ToString() ?? "EnPreparacion",
                FechaPedido = selected.FechaPedido,
                FechaActualizacion = DateTime.Now,
                CuponId = null,
                Notas = txtNotas.Text
            };

            var (exito, mensaje) = await _pedidoService.ActualizarAsync(dto);
            MessageBox.Show(mensaje, exito ? "Éxito" : "Error", MessageBoxButtons.OK, exito ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            if (exito)
            {
                CargarPedidosAsync();
                LimpiarCampos();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            CargarPedidosAsync();
        }
    }
}
