using System;
using System.Windows.Forms;
using SellPoint.View.Helpers;
using SellPoint.View.Mappers;
using SellPoint.View.Models.Pedido;
using SellPoint.View.Models.ViewModels;
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

            CargarUsuariosCombo();
            CargarDireccionesCombo();
            CargarMetodoPagoCombo();
            CargarEstadoCombo();

            CargarPedidosAsync();
        }

        #region Métodos de carga de combos

        private void CargarUsuariosCombo()
        {
            cmbUsuarios.Items.Clear();
            int[] idsUsuarios = { 2, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
            cmbUsuarios.Items.AddRange(idsUsuarios.Cast<object>().ToArray());
            cmbUsuarios.SelectedIndex = 0;
        }

        private void CargarDireccionesCombo()
        {
            cmbDirecciones.Items.Clear();
            cmbDirecciones.Items.Add(1); // Solo existe la 1 por ahora
            cmbDirecciones.SelectedIndex = 0;
        }

        private void CargarMetodoPagoCombo()
        {
            cmbMetodoPago.Items.Clear();
            cmbMetodoPago.Items.AddRange(new[] { "PayPal", "TransferenciaBancaria", "Tarjeta" });
            cmbMetodoPago.SelectedIndex = 0;
        }

        private void CargarEstadoCombo()
        {
            cmbEstado.Items.Clear();
            cmbEstado.Items.AddRange(new[] { "EnPreparacion", "Enviado", "Entregado", "Cancelado" });
            cmbEstado.SelectedIndex = 0;
        }

        #endregion

        #region Métodos auxiliares

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

        #endregion

        #region CRUD

        private async void CargarPedidosAsync()
        {
            var respuesta = await _pedidoService.ObtenerTodosAsync();
            dgvPedidos.DataSource = respuesta.Data ?? new List<PedidoDTO>();

            if (!respuesta.IsSuccess)
                MessageBoxHelper.MostrarAdvertencia(respuesta.Message, PedidoMensajes.Advertencia);

            dgvPedidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvPedidos.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            var vm = PedidoFormMapper.FormToViewModel(this);
            vm.FechaPedido = DateTime.Now; 
            var dto = PedidoViewModelMapper.ToSaveDTO(vm);

            var respuesta = await _pedidoService.AgregarAsync(dto);

            if (respuesta.IsSuccess)
            {
                MessageBoxHelper.MostrarExito(respuesta.Message, PedidoMensajes.Exito);
                CargarPedidosAsync();
                LimpiarCampos();
            }
            else
            {
                MessageBoxHelper.MostrarError(respuesta.Message, PedidoMensajes.Error);
            }
        }

        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvPedidos.CurrentRow?.DataBoundItem is not PedidoDTO dtoSeleccionado)
                return;
            var vm = PedidoFormMapper.FormToViewModel(this);
            vm.Id = dtoSeleccionado.Id;
            vm.FechaPedido = dtoSeleccionado.FechaPedido;

            var dto = PedidoViewModelMapper.ToUpdateDTO(vm);

            var respuesta = await _pedidoService.ActualizarAsync(dto);

            if (respuesta.IsSuccess)
            {
                MessageBoxHelper.MostrarExito(respuesta.Message, PedidoMensajes.Exito);
                CargarPedidosAsync();
                LimpiarCampos();
            }
            else
            {
                MessageBoxHelper.MostrarError(respuesta.Message, PedidoMensajes.Error);
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvPedidos.CurrentRow?.DataBoundItem is not PedidoDTO pedido)
                return;

            var (valido, mensaje) = RemovePedidoValidator.Validar(pedido.Id);
            if (!valido)
            {
                MessageBoxHelper.MostrarAdvertencia(mensaje, PedidoMensajes.Validacion);
                return;
            }

            if (MessageBoxHelper.MostrarPregunta(PedidoMensajes.ConfirmarEliminacion, PedidoMensajes.Advertencia))
            {
                var respuesta = await _pedidoService.EliminarAsync(pedido.Id);

                if (respuesta.IsSuccess)
                {
                    MessageBoxHelper.MostrarExito(respuesta.Message, PedidoMensajes.Exito);
                    CargarPedidosAsync();
                }
                else
                {
                    MessageBoxHelper.MostrarError(respuesta.Message, PedidoMensajes.Error);
                }
            }
        }

        private async void btnCargar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtBuscarId.Text) && int.TryParse(txtBuscarId.Text, out int id))
            {
                var (valido, mensaje) = PedidoIdValidator.Validar(id);
                if (!valido)
                {
                    MessageBoxHelper.MostrarAdvertencia(mensaje, PedidoMensajes.Validacion);
                    return;
                }

                var respuesta = await _pedidoService.ObtenerPorIdAsync(id);
                if (respuesta.IsSuccess && respuesta.Data != null)
                {
                    var vm = PedidoViewModelMapper.ToViewModel(respuesta.Data);
                    PedidoFormMapper.ViewModelToForm(vm, this);

                    dgvPedidos.DataSource = new List<PedidoDTO> { respuesta.Data };
                    MessageBoxHelper.MostrarExito(PedidoMensajes.PedidoCargadoCorrectamente, PedidoMensajes.Exito);
                }
                else
                {
                    MessageBoxHelper.MostrarAdvertencia(respuesta.Message, PedidoMensajes.Advertencia);
                }

                txtBuscarId.Text = "";
            }
            else
            {
                CargarPedidosAsync();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        #endregion

        #region Mapear datos seleccionados del DataGridView

        private void dgvPedidos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPedidos.CurrentRow?.DataBoundItem is not PedidoDTO dto)
                return;
            var vm = PedidoViewModelMapper.ToViewModel(dto);
            PedidoFormMapper.ViewModelToForm(vm, this);
        }

        #endregion
    }
}