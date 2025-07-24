
using SellPoint.View.Models.ModelsCupon;
using SellPoint.View.Services.CuponApiClient;
using System;
using System.Linq;
using System.Windows.Forms;

namespace SellPoint.View
{
    public partial class CuponForm : Form
    {
        private readonly CuponApiClient _cuponService;

        public CuponForm(CuponApiClient cuponService)
        {
            InitializeComponent();
            _cuponService = new CuponApiClient(new HttpClient());
            _cuponService = cuponService;
        }

        private async void CuponForm_Load(object sender, EventArgs e)
        {
            await CargarCupones();
        }

        private async void btnCargar_Click(object sender, EventArgs e)
        {
            await CargarCupones();
        }

        private async Task CargarCupones()
        {
            var cupones = await _cuponService.ObtenerTodosAsync();
            dgvCupones.DataSource = cupones.ToList();
        }

        private async void btnCrear_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos(out string mensaje))
            {
                MessageBox.Show(mensaje, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dto = new SaveCuponDTO
            {
                Codigo = txtCodigo.Text,
                Descripcion = "Auto generado",
                TipoDescuento = "porcentaje",
                ValorDescuento = decimal.Parse(txtDescuento.Text),
                MontoMinimo = 0,
                FechaInicio = DateTime.Now,
                FechaVencimiento = DateTime.Parse(txtFechaVencimiento.Text),
                UsosMaximos = null,
                Activo = true
            };

            var resultado = await _cuponService.CrearAsync(dto);
            MessageBox.Show(resultado ? "Cupón creado." : "Error al crear.");
            await CargarCupones();
        }

        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("Debe ingresar un ID válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidarCampos(out string mensaje))
            {
                MessageBox.Show(mensaje, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dto = new UpdateCuponDTO
            {
                Id = id,
                Codigo = txtCodigo.Text,
                Descripcion = "Actualizado",
                TipoDescuento = "porcentaje",
                ValorDescuento = decimal.Parse(txtDescuento.Text),
                MontoMinimo = 0,
                FechaInicio = DateTime.Now,
                FechaVencimiento = DateTime.Parse(txtFechaVencimiento.Text),
                UsosMaximos = null,
                Activo = true
            };

            var resultado = await _cuponService.ActualizarAsync(dto);
            MessageBox.Show(resultado ? "Cupón actualizado." : "Error al actualizar.");
            await CargarCupones();
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("Debe ingresar un ID válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dto = new RemoveCuponDTO { Id = id };
            var resultado = await _cuponService.EliminarAsync(dto);
            MessageBox.Show(resultado ? "Cupón eliminado." : "Error al eliminar.");
            await CargarCupones();
        }

        private bool ValidarCampos(out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                mensaje = "El campo 'Código' es obligatorio.";
                return false;
            }

            if (!decimal.TryParse(txtDescuento.Text, out _))
            {
                mensaje = "Debe ingresar un valor numérico válido para 'Descuento'.";
                return false;
            }

            if (!DateTime.TryParse(txtFechaVencimiento.Text, out _))
            {
                mensaje = "Debe ingresar una fecha válida para 'Fecha de vencimiento'.";
                return false;
            }

            return true;
        }
    }
}

