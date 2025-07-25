using SellPoint.Aplication.Dtos.Cupon;
using SellPoint.View.Services.CuponApiClient;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SellPoint.View
{
    public partial class CuponForm : Form
    {
        private readonly ICuponApiClient _cuponApiClient;
        private readonly ErrorProvider _errorProvider = new ErrorProvider();

        public CuponForm(ICuponApiClient cuponApiClient)
        {
            InitializeComponent();
            _cuponApiClient = cuponApiClient;
        }

        private async void CuponForm_Load(object sender, EventArgs e)
        {
            await CargarCuponesAsync();
        }

        private async void btnCargar_Click(object sender, EventArgs e)
        {
            await CargarCuponesAsync();
        }

        private async void btnCrear_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario()) return;

            var dto = new SaveCuponDTO
            {
                Codigo = txtCodigo.Text.Trim(),
                ValorDescuento = decimal.Parse(txtValor.Text.Trim()),
                FechaVencimiento = dtpFecha.Value
            };

            bool creado = await _cuponApiClient.CrearAsync(dto);
            if (creado)
            {
                MessageBox.Show("Cupón creado con éxito.");
                await CargarCuponesAsync();
                LimpiarFormulario();
            }
            else
            {
                MessageBox.Show("Error al crear el cupón.");
            }
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
                ValorDescuento = decimal.Parse(txtValor.Text.Trim()),
                FechaVencimiento = dtpFecha.Value
            };

            bool actualizado = await _cuponApiClient.ActualizarAsync(dto);
            if (actualizado)
            {
                MessageBox.Show("Cupón actualizado con éxito.");
                await CargarCuponesAsync();
                LimpiarFormulario();
            }
            else
            {
                MessageBox.Show("Error al actualizar el cupón.");
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("ID inválido.");
                return;
            }

            var dto = new RemoveCuponDTIO { Id = id };
            bool eliminado = await _cuponApiClient.EliminarAsync(dto);
            if (eliminado)
            {
                MessageBox.Show("Cupón eliminado con éxito.");
                await CargarCuponesAsync();
                LimpiarFormulario();
            }
            else
            {
                MessageBox.Show("Error al eliminar el cupón.");
            }
        }

        private async Task CargarCuponesAsync()
        {
            try
            {
                var cupones = await _cuponApiClient.ObtenerTodosAsync();
                dgvCupones.DataSource = cupones.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar cupones: " + ex.Message);
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

            if (!decimal.TryParse(txtValor.Text, out _))
            {
                _errorProvider.SetError(txtValor, "Valor inválido.");
                valido = false;
            }

            return valido;
        }

        private void LimpiarFormulario()
        {
            txtId.Text = "";
            txtCodigo.Text = "";
            txtValor.Text = "";
            dtpFecha.Value = DateTime.Now;
            _errorProvider.Clear();
        }
    }
}


