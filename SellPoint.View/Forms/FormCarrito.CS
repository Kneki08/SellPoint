using SellPoint.Aplication.Dtos.Carrito;
using SellPoint.View.Models.ModelsCarito;
using SellPoint.View.Service.ServiceCarrito;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SellPoint.View
{
    public partial class FormCarrito : Form
    {
        private readonly ICarritoApiClient _carritoApiClient;
        private readonly ErrorProvider _errorProvider = new ErrorProvider();

        public FormCarrito(ICarritoApiClient carritoApiClient)
        {
            InitializeComponent();
            _carritoApiClient = carritoApiClient;
        }

        private async void FormCarrito_Load(object sender, EventArgs e)
        {
            await CargarCarritosAsync();
        }

        private async void btnCargar_Click(object sender, EventArgs e)
        {
            await CargarCarritosAsync();
        }

        private async void btnCrear_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario()) return;

            var dto = new SaveCarritoDTO
            {
                ClienteId = int.Parse(txtClienteId.Text.Trim()),
                FechaCreacion = dtpFechaCreacion.Value,
                Estado = txtEstado.Text.Trim()
            };

            bool creado = await _carritoApiClient.CrearAsync(dto);
            if (creado)
            {
                MessageBox.Show("Carrito creado con éxito.");
                await CargarCarritosAsync();
                LimpiarFormulario();
            }
            else
            {
                MessageBox.Show("Error al crear el carrito.");
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

            var dto = new UpdateCarritoDTO
            {
                Id = id,
                ClienteId = int.Parse(txtClienteId.Text.Trim()),
                FechaCreacion = dtpFechaCreacion.Value,
                Estado = txtEstado.Text.Trim()
            };

            bool actualizado = await _carritoApiClient.ActualizarAsync(dto);
            if (actualizado)
            {
                MessageBox.Show("Carrito actualizado con éxito.");
                await CargarCarritosAsync();
                LimpiarFormulario();
            }
            else
            {
                MessageBox.Show("Error al actualizar el carrito.");
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("ID inválido.");
                return;
            }

            var dto = new RemoveCarritoDTO { Id = id };
            bool eliminado = await _carritoApiClient.EliminarAsync(dto);
            if (eliminado)
            {
                MessageBox.Show("Carrito eliminado con éxito.");
                await CargarCarritosAsync();
                LimpiarFormulario();
            }
            else
            {
                MessageBox.Show("Error al eliminar el carrito.");
            }
        }

        private async Task CargarCarritosAsync()
        {
            try
            {
                var carritosDTO = await _carritoApiClient.ObtenerTodosAsync();
                var carritosModel = carritosDTO.Select(dto => new CarritoModel
                {
                    Id = dto.Id,
                    ClienteId = dto.ClienteId,
                    FechaAgregado = dto.FechaAgregado,
                    Estado = dto.Estado
                }).ToList();

                dgvCarritos.DataSource = carritosModel;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar carritos: " + ex.Message);
            }
        }

        private bool ValidarFormulario()
        {
            _errorProvider.Clear();
            bool valido = true;

            if (string.IsNullOrWhiteSpace(txtClienteId.Text) || !int.TryParse(txtClienteId.Text, out _))
            {
                _errorProvider.SetError(txtClienteId, "Cliente ID válido requerido.");
                valido = false;
            }

            if (string.IsNullOrWhiteSpace(txtEstado.Text))
            {
                _errorProvider.SetError(txtEstado, "Estado es obligatorio.");
                valido = false;
            }

            return valido;
        }

        private void LimpiarFormulario()
        {
            txtId.Text = "";
            txtClienteId.Text = "";
            txtEstado.Text = "";
            dtpFechaCreacion.Value = DateTime.Now;
            _errorProvider.Clear();
        }
    }
}
