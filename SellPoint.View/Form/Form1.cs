using SellPoint.Aplication.Dtos.DetallePedido;
using SellPoint.View.Models.ModelDetallePedido;
using System.Windows.Forms;
using Nest;
using System.Net.Http;
using SellPoint.View.Service.DetallePedidoClient.Implement;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;
using SellPoint.View.Validations;
using SellPoint.View.Repositories;

namespace SellPoint.View
{
    public partial class Form1 : Form
    {
        private readonly IDetallePedidoRepository _repository;

        public Form1( IDetallePedidoRepository repository)
        {
            InitializeComponent();
            _repository = repository;
            ConfigurarInterfaz();
            _ = VerificarAPIAsync();
            ConfigurarDataGridView();
        }

        private async Task VerificarAPIAsync()
        {
            try
            {
                var response = await _repository.GetAllAsync();
                if (!response.Success)
                {
                    DeshabilitarControles();
                    MessageBox.Show("API no responde correctamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                DeshabilitarControles();
                MessageBox.Show("No se pudo conectar al API", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarInterfaz()
        {
            dgvDetallePedido.AutoGenerateColumns = false;
            dgvDetallePedido.Columns.Add("Id", "ID");
            dgvDetallePedido.Columns.Add("PedidoId", "ID Pedido");
            dgvDetallePedido.Columns.Add("ProductoId", "ID Producto");
            dgvDetallePedido.Columns.Add("Cantidad", "Cantidad");
            dgvDetallePedido.Columns.Add("PrecioUnitario", "Precio Unitario");

            dgvDetallePedido.SelectionChanged += (s, e) =>
            {
                if (dgvDetallePedido.SelectedRows.Count > 0)
                {
                    var fila = dgvDetallePedido.SelectedRows[0];
                    txtId.Text = fila.Cells["Id"].Value?.ToString() ?? "";
                    txtPedidoId.Text = fila.Cells["PedidoId"].Value?.ToString() ?? "";
                    txtProductoId.Text = fila.Cells["ProductoId"].Value?.ToString() ?? "";
                    txtCantidad.Text = fila.Cells["Cantidad"].Value?.ToString() ?? "";
                    txtPrecio.Text = fila.Cells["PrecioUnitario"].Value?.ToString() ?? "";
                }
            };
        }

        private void DeshabilitarControles()
        {
            btnCargar.Enabled = false;
            btnCrear.Enabled = false;
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void ConfigurarDataGridView()
        {
            dgvDetallePedido.AutoGenerateColumns = false;
            dgvDetallePedido.Columns.Clear();

            var colId = new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "Id", Name = "Id" };
            var colPedidoId = new DataGridViewTextBoxColumn { HeaderText = "ID Pedido", DataPropertyName = "PedidoId", Name = "PedidoId" };
            var colProductoId = new DataGridViewTextBoxColumn { HeaderText = "ID Producto", DataPropertyName = "ProductoId", Name = "ProductoId" };
            var colCantidad = new DataGridViewTextBoxColumn { HeaderText = "Cantidad", DataPropertyName = "Cantidad", Name = "Cantidad" };
            var colPrecio = new DataGridViewTextBoxColumn
            {
                HeaderText = "Precio Unitario",
                DataPropertyName = "PrecioUnitario",
                Name = "PrecioUnitario",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            };

            dgvDetallePedido.Columns.AddRange(colId, colPedidoId, colProductoId, colCantidad, colPrecio);
        }

        private async void btnCargar_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                btnCargar.Enabled = false;

                var response = await _repository.GetAllAsync();

                if (response.Success && response.Data != null)
                {
                    Console.WriteLine($"Datos recibidos: {JsonSerializer.Serialize(response.Data)}");
                    dgvDetallePedido.DataSource = response.Data.ToList();
                    dgvDetallePedido.AutoResizeColumns();
                }
                else
                {
                    MessageBox.Show(response.Message ?? "No se recibieron datos", "Error al cargar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                btnCargar.Enabled = true;
            }
        }

        private async void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos()) return;

                var nuevo = new SaveDetallePedidoDTO
                {
                    PedidoId = int.Parse(txtPedidoId.Text),
                    ProductoId = int.Parse(txtProductoId.Text),
                    Cantidad = int.Parse(txtCantidad.Text),
                    PrecioUnitario = decimal.Parse(txtPrecio.Text)
                };

                var response = await _repository.CreateAsync(nuevo);

                if (response.Success)
                {
                    MessageBox.Show("Detalle creado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await RecargarDatosAsync();
                }
                else
                {
                    MessageBox.Show(response.Message, "Error al crear", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ValidationException vex)
            {
                MessageBox.Show(vex.ValidationResult.ErrorMessage, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos() || string.IsNullOrEmpty(txtId.Text))
                {
                    MessageBox.Show("Seleccione un registro y complete los campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var actualizado = new UpdateDetallePedidoDTO
                {
                    Id = int.Parse(txtId.Text),
                    PedidoId = int.Parse(txtPedidoId.Text),
                    ProductoId = int.Parse(txtProductoId.Text),
                    Cantidad = int.Parse(txtCantidad.Text),
                    PrecioUnitario = decimal.Parse(txtPrecio.Text)
                };

                var response = await _repository.UpdateAsync(actualizado);

                if (response.Success)
                {
                    MessageBox.Show("Detalle actualizado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await RecargarDatosAsync();
                }
                else
                {
                    MessageBox.Show(response.Message, "Error al actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ValidationException vex)
            {
                MessageBox.Show(vex.ValidationResult.ErrorMessage, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Seleccione un registro para eliminar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Confirmar eliminación?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                var response = await _repository.DeleteAsync(int.Parse(txtId.Text));

                if (response.Success)
                {
                    MessageBox.Show("Detalle eliminado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await RecargarDatosAsync();
                }
                else
                {
                    MessageBox.Show(response.Message, "Error al eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task RecargarDatosAsync()
        {
            try
            {
                var response = await _repository.GetAllAsync();
                if (response.Success)
                {
                    dgvDetallePedido.DataSource = response.Data.ToList();
                    LimpiarFormulario();
                }
                else
                {
                    MessageBox.Show(response.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtPedidoId.Text) ||
                string.IsNullOrWhiteSpace(txtProductoId.Text) ||
                string.IsNullOrWhiteSpace(txtCantidad.Text) ||
                string.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                MessageBox.Show("Todos los campos son requeridos", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(txtPedidoId.Text, out _) ||
                !int.TryParse(txtProductoId.Text, out _) ||
                !int.TryParse(txtCantidad.Text, out _) ||
                !decimal.TryParse(txtPrecio.Text, out _))
            {
                MessageBox.Show("Ingrese valores numéricos válidos", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void LimpiarFormulario()
        {
            txtId.Clear();
            txtPedidoId.Clear();
            txtProductoId.Clear();
            txtCantidad.Clear();
            txtPrecio.Clear();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // Configurar el evento de selección del DataGridView
          
        }
    }
}
