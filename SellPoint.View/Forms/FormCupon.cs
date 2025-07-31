using System.Runtime.Versioning; 
using SellPoint.Aplication.Dtos.Cupon;
using SellPoint.View.Models.ModelsCupon;
using SellPoint.View.Services.CuponApiClient;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SellPoint.View
{
    [SupportedOSPlatform("windows")]
    public partial class FormCupon : Form
    {
        private readonly ICuponApiClient _cuponApiClient;
        private readonly ErrorProvider _errorProvider = new ErrorProvider();

        public FormCupon(ICuponApiClient cuponApiClient)
        {
            InitializeComponent();
            _cuponApiClient = cuponApiClient;
        }

        private async void FormCupon_Load(object sender, EventArgs e) => await CargarCuponesAsync();

        private async void btnCargar_Click(object sender, EventArgs e) => await CargarCuponesAsync();

        private async void btnCrear_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario()) return;

            var dto = new SaveCuponDTO
            {
                Codigo = txtCodigo.Text.Trim(),
                ValorDescuento = decimal.Parse(txtDescuento.Text),
                FechaVencimiento = dtpFechaVencimiento.Value
            };

            await ProcesarResultadoAsync(await _cuponApiClient.CrearAsync(dto), "creado");
        }

        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("ID inválido.");
                return;
            }

            if (!ValidarFormulario()) return;

            var dto = new UpdateCuponDTO
            {
                Id = id,
                Codigo = txtCodigo.Text.Trim(),
                ValorDescuento = decimal.Parse(txtDescuento.Text),
                FechaVencimiento = dtpFechaVencimiento.Value
            };

            await ProcesarResultadoAsync(await _cuponApiClient.ActualizarAsync(dto), "actualizado");
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("ID inválido.");
                return;
            }

            var dto = new RemoveCuponDTIO { Id = id };
            await ProcesarResultadoAsync(await _cuponApiClient.EliminarAsync(dto), "eliminado");
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

        private bool ValidarFormulario()
        {
            _errorProvider.Clear();
            bool valido = true;

            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                _errorProvider.SetError(txtCodigo, "El código es obligatorio.");
                valido = false;
            }

            if (string.IsNullOrWhiteSpace(txtDescuento.Text) || !decimal.TryParse(txtDescuento.Text, out _))
            {
                _errorProvider.SetError(txtDescuento, "El descuento debe ser numérico.");
                valido = false;
            }

            return valido;
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




