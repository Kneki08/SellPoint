using System.Runtime.Versioning;
using SellPoint.View.Helpers;
using SellPoint.View.Models.ModelsCupon;
using SellPoint.View.Services.CuponApiClient;

namespace SellPoint.View
{
    [SupportedOSPlatform("windows")]
    public partial class FormCupon : Form
    {
        private readonly ICuponApiClient _cuponApiClient;
        private readonly ErrorProvider _errorProvider = new();

        public FormCupon(ICuponApiClient cuponApiClient)
        {
            InitializeComponent();
            _cuponApiClient = cuponApiClient;

            Load += FormCupon_Load;
            btnCargar.Click += btnCargar_Click;
            btnCrear.Click += btnCrear_Click;
            btnActualizar.Click += btnActualizar_Click;
            btnEliminar.Click += btnEliminar_Click;
        }

        private async void FormCupon_Load(object? sender, EventArgs e) => await CargarCuponesAsync();

        private async void btnCargar_Click(object? sender, EventArgs e) => await CargarCuponesAsync();

        private async void btnCrear_Click(object? sender, EventArgs e)
        {
            if (!CuponFormHelper.ValidarFormulario(txtCodigo, txtDescuento, _errorProvider)) return;

            var model = CuponFormHelper.ConstruirSaveModel(txtCodigo, txtDescuento, dtpFechaVencimiento);
            await ProcesarResultadoAsync(await _cuponApiClient.CrearAsync(model), "creado");
        }

        private async void btnActualizar_Click(object? sender, EventArgs e)
        {
            var model = CuponFormHelper.ConstruirUpdateModel(txtId, txtCodigo, txtDescuento, dtpFechaVencimiento);
            if (model == null)
            {
                MessageBox.Show("ID inválido.");
                return;
            }

            if (!CuponFormHelper.ValidarFormulario(txtCodigo, txtDescuento, _errorProvider)) return;

            await ProcesarResultadoAsync(await _cuponApiClient.ActualizarAsync(model), "actualizado");
        }

        private async void btnEliminar_Click(object? sender, EventArgs e)
        {
            var model = CuponFormHelper.ConstruirRemoveModel(txtId);
            if (model == null)
            {
                MessageBox.Show("ID inválido.");
                return;
            }

            await ProcesarResultadoAsync(await _cuponApiClient.EliminarAsync(model), "eliminado");
        }

        private async Task CargarCuponesAsync()
        {
            try
            {
                var cuponesDTO = await _cuponApiClient.ObtenerTodosAsync();
                dgvCupones.DataSource = cuponesDTO.Select(dto => new CuponModel
                {
                    Id = dto.Id,
                    Codigo = dto.Codigo,
                    ValorDescuento = dto.ValorDescuento,
                    FechaVencimiento = dto.FechaVencimiento
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar cupones: " + ex.Message);
            }
        }

        private async Task ProcesarResultadoAsync(bool resultado, string accion)
        {
            if (resultado)
            {
                MessageBox.Show($"Cupón {accion} con éxito.");
                await CargarCuponesAsync();
                LimpiarFormulario();
            }
            else
            {
                MessageBox.Show($"Error al {accion} el cupón.");
            }
        }

        private void LimpiarFormulario()
        {
            txtId.Text = "";
            txtCodigo.Text = "";
            txtDescuento.Text = "";
            dtpFechaVencimiento.Value = DateTime.Today;
            _errorProvider.Clear();
        }
    }
}





