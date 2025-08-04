using System;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;
using SellPoint.View.Factories;
using SellPoint.View.Helpers;
using SellPoint.View.Mappers;
using SellPoint.View.Models.Pedido;
using SellPoint.View.Models.ViewModels;
using SellPoint.View.Services.Pedido;
using SellPoint.View.Services.Pedido.Campos.Service;
using SellPoint.View.Services.Pedido.Pedido.Service;
using SellPoint.View.Validations;

namespace SellPoint.View.Forms
{
    public partial class PedidoForm : Form
    {
        private readonly IPedidoService _pedidoService;
        private readonly IPedidoDtoFactory _dtoFactory;
        private readonly IPedidoFormMapper _formMapper;
        private readonly IPedidoViewModelMapper _viewModelMapper;
        private readonly IPedidoValidator _validator;
        private readonly IPedidoCamposService _camposService;
        private readonly IPedidoOpcionesService _opcionesService;
        private readonly ILogger<PedidoForm> _logger;

        public PedidoForm(
            IPedidoService pedidoService,
            IPedidoDtoFactory dtoFactory,
            IPedidoFormMapper formMapper,
            IPedidoViewModelMapper viewModelMapper,
            IPedidoValidator validator,
            IPedidoCamposService camposService,
            IPedidoOpcionesService opcionesService,
            ILogger<PedidoForm> logger)
        {
            _pedidoService = pedidoService;
            _dtoFactory = dtoFactory;
            _formMapper = formMapper;
            _viewModelMapper = viewModelMapper;
            _validator = validator;
            _camposService = camposService;
            _opcionesService = opcionesService;
            _logger = logger;

            InitializeComponent();

            // Cargar combos desde API
            _ = CargarUsuariosComboAsync();
            _ = CargarDireccionesComboAsync();
            _ = CargarMetodoPagoComboAsync();
            _ = CargarEstadoComboAsync();

            CargarPedidosAsync();
        }

        private async Task CargarUsuariosComboAsync()
        {
            cmbUsuarios.Items.Clear();
            var usuarios = await _opcionesService.ObtenerUsuariosAsync();
            cmbUsuarios.Items.AddRange(usuarios.Cast<object>().ToArray());
            if (usuarios.Any())
                cmbUsuarios.SelectedIndex = 0;
        }

        private async Task CargarDireccionesComboAsync()
        {
            cmbDirecciones.Items.Clear();
            var direcciones = await _opcionesService.ObtenerDireccionesAsync();
            cmbDirecciones.Items.AddRange(direcciones.Cast<object>().ToArray());
            if (direcciones.Any())
                cmbDirecciones.SelectedIndex = 0;
        }

        private async Task CargarMetodoPagoComboAsync()
        {
            cmbMetodoPago.Items.Clear();
            var metodos = await _opcionesService.ObtenerMetodosPagoAsync();
            cmbMetodoPago.Items.AddRange(metodos.Cast<object>().ToArray());
            if (metodos.Any())
                cmbMetodoPago.SelectedIndex = 0;
        }

        private async Task CargarEstadoComboAsync()
        {
            cmbEstado.Items.Clear();
            var estados = await _opcionesService.ObtenerEstadosAsync();
            cmbEstado.Items.AddRange(estados.Cast<object>().ToArray());
            if (estados.Any())
                cmbEstado.SelectedIndex = 0;
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
            var campos = _camposService.TryParseCampos(
                cmbUsuarios.SelectedItem?.ToString() ?? "",
                cmbDirecciones.SelectedItem?.ToString() ?? "",
                txtSubtotal.Text,
                txtDescuento.Text,
                txtCostoEnvio.Text,
                txtTotal.Text
            );

            if (!campos.Success)
            {
                MessageBoxHelper.MostrarAdvertencia(campos.Message, PedidoMensajes.Validacion);
                return;
            }

            var dto = _dtoFactory.CrearSaveDTO(
                campos,
                cmbMetodoPago.SelectedItem?.ToString() ?? "PayPal",
                txtReferencia.Text ?? "",
                cmbEstado.SelectedItem?.ToString() ?? "EnPreparacion",
                txtNotas.Text ?? ""
            );

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

            var campos = _camposService.TryParseCampos(
                cmbUsuarios.SelectedItem?.ToString() ?? "",
                cmbDirecciones.SelectedItem?.ToString() ?? "",
                txtSubtotal.Text,
                txtDescuento.Text,
                txtCostoEnvio.Text,
                txtTotal.Text
            );

            if (!campos.Success)
            {
                MessageBoxHelper.MostrarAdvertencia(campos.Message, PedidoMensajes.Validacion);
                return;
            }

            var dto = _dtoFactory.CrearUpdateDTO(
                campos,
                dtoSeleccionado,
                cmbMetodoPago.SelectedItem?.ToString() ?? "PayPal",
                txtReferencia.Text ?? "",
                cmbEstado.SelectedItem?.ToString() ?? "EnPreparacion",
                txtNotas.Text ?? ""
            );

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

            var (valido, mensaje) = _validator.ValidarEliminar(pedido.Id);
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
                var (valido, mensaje) = _validator.ValidarId(id);
                if (!valido)
                {
                    MessageBoxHelper.MostrarAdvertencia(mensaje, PedidoMensajes.Validacion);
                    return;
                }

                var respuesta = await _pedidoService.ObtenerPorIdAsync(id);
                if (respuesta.IsSuccess && respuesta.Data != null)
                {
                    var vm = _viewModelMapper.ToViewModel(respuesta.Data);
                    _formMapper.ViewModelToForm(vm, this);

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

        private void dgvPedidos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPedidos.CurrentRow?.DataBoundItem is not PedidoDTO dto)
                return;

            var vm = _viewModelMapper.ToViewModel(dto);
            _formMapper.ViewModelToForm(vm, this);
        }
    }
}
