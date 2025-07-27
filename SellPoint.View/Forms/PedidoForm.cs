using System;
using System.Windows.Forms;
using SellPoint.View.Models.Pedido;
using SellPoint.View.Services.Pedido;
using SellPoint.View.Validations;

namespace SellPoint.View.Forms
{
    public partial class PedidoForm : Form
    {
        private readonly IPedidoService _pedidoService;

        public PedidoForm(IPedidoService pedidoService)
        {   
            _pedidoService = pedidoService;
            InitializeComponent();

            cmbEstado.Items.AddRange(new[] { "EnPreparacion", "Enviado", "Entregado", "Cancelado" });
            cmbEstado.SelectedIndex = 0;

            CargarPedidosAsync();
        }

        private async void CargarPedidosAsync()
        {
            var respuesta = await _pedidoService.ObtenerTodosAsync();
            dgvPedidos.DataSource = respuesta.Data ?? new List<PedidoDTO>();

            if (!respuesta.IsSuccess)
                MessageBox.Show(respuesta.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            dgvPedidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvPedidos.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void LimpiarCampos()
        {
            foreach (Control c in groupBoxDatos.Controls)
            {
                if (c is TextBox tb)
                    tb.Text = "";
                else if (c is ComboBox cb && cb.Items.Count > 0)
                    cb.SelectedIndex = 0;
            }
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            var campos = PedidoCamposParser.TryParseCampos(
                txtIdUsuario.Text, txtDireccion.Text, txtSubtotal.Text, txtDescuento.Text, txtCostoEnvio.Text, txtTotal.Text);

            if (!campos.Success)
            {
                MessageBox.Show(campos.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var dto = PedidoDtoFactory.CrearSaveDTO(
                campos,
                txtMetodoPago.Text,
                txtReferencia.Text,
                cmbEstado.SelectedItem?.ToString() ?? "EnPreparacion",
                txtNotas.Text
            );

            var (valido, mensaje) = SavePedidoValidator.Validar(dto);
            if (!valido)
            {
                MessageBox.Show(mensaje, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var respuesta = await _pedidoService.AgregarAsync(dto);
            MessageBox.Show(respuesta.Message, respuesta.IsSuccess ? "Éxito" : "Error", MessageBoxButtons.OK, respuesta.IsSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (respuesta.IsSuccess)
            {
                CargarPedidosAsync();
                LimpiarCampos();
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvPedidos.CurrentRow?.DataBoundItem is not PedidoDTO pedido)
                return;
            
            var (valido, mensaje) = RemovePedidoValidator.Validar(pedido.Id);
            if (!valido)
            {
                MessageBox.Show(mensaje, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var confirmado = MessageBox.Show("¿Seguro que deseas eliminar este pedido?", "Confirmar", MessageBoxButtons.YesNo);
            if (confirmado == DialogResult.Yes)
            {
                var respuesta = await _pedidoService.EliminarAsync(pedido.Id);
                MessageBox.Show(respuesta.Message, respuesta.IsSuccess ? "Éxito" : "Error", MessageBoxButtons.OK, respuesta.IsSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                if (respuesta.IsSuccess)
                    CargarPedidosAsync();
            }
        }

        private void dgvPedidos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPedidos.CurrentRow?.DataBoundItem is not PedidoDTO pedido)
                return;

            PedidoUiMapper.CargarEnCampos(pedido, this);
        }

        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvPedidos.CurrentRow?.DataBoundItem is not PedidoDTO selected)
                return;

            var campos = PedidoCamposParser.TryParseCampos(
                txtIdUsuario.Text, txtDireccion.Text, txtSubtotal.Text, txtDescuento.Text, txtCostoEnvio.Text, txtTotal.Text);

            if (!campos.Success)
            {
                MessageBox.Show(campos.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dto = PedidoDtoFactory.CrearUpdateDTO(
                campos,
                selected,
                txtMetodoPago.Text,
                txtReferencia.Text,
                cmbEstado.SelectedItem?.ToString() ?? "EnPreparacion",
                txtNotas.Text
            );

            var (valido, mensaje) = UpdatePedidoValidator.Validar(dto);
            if (!valido)
            {
                MessageBox.Show(mensaje, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var respuesta = await _pedidoService.ActualizarAsync(dto);
            MessageBox.Show(respuesta.Message, respuesta.IsSuccess ? "Éxito" : "Error", MessageBoxButtons.OK, respuesta.IsSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (respuesta.IsSuccess)
            {
                CargarPedidosAsync();
                LimpiarCampos();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private async void btnCargar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtBuscarId.Text) && int.TryParse(txtBuscarId.Text, out int id))
            {
                var (valido, mensaje) = PedidoIdValidator.Validar(id);
                if (!valido)
                {
                    MessageBox.Show(mensaje, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                        
                var respuesta = await _pedidoService.ObtenerPorIdAsync(id);
                if (respuesta.IsSuccess && respuesta.Data != null)
                {
                    dgvPedidos.DataSource = new List<PedidoDTO> { respuesta.Data };
                    MessageBox.Show("Pedido cargado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(respuesta.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);  
                }

                txtBuscarId.Text = ""; // limpiar después de cargar
            }
            else
            {
                CargarPedidosAsync(); // si está vacío o inválido, se cargan todos
            }
        }
    }
}