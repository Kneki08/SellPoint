using SellPoint.View.Models.ModelDetallePedido.Dtos;
using SellPoint.View.Service;
using System.Net.Http;

namespace SellPoint.View
{
    public partial class Form1 : Form
    {
        private readonly IDetallePedidoRepository _repository;

        public Form1(IDetallePedidoRepository repository)
        {
            InitializeComponent();
            _repository = repository;
            ConfigurarInterfaz();
            _ = VerificarAPIAsync();
        }

        private async Task VerificarAPIAsync()
        {
            try
            {
                var response = await _repository.GetAllAsync();

                if (!response.Success)
                {
                    DeshabilitarControles();
                    MessageBox.Show($"Error al conectar con la API: {response.Message}",
                                  "Error de API",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Error);
                }
            }
            catch (HttpRequestException ex)
            {
                DeshabilitarControles();
                MessageBox.Show($"No se pudo conectar con la API: {ex.Message}\n\nVerifique que la API esté corriendo.",
                              "Error de conexión",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        
        }

        private void ConfigurarInterfaz()
        {
            // Configuración básica del DataGridView
            dgvDetallePedido.AutoGenerateColumns = false;
            dgvDetallePedido.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Configurar columnas
            var columns = new[]
            {
                new DataGridViewTextBoxColumn { Name = "Id", HeaderText = "ID", DataPropertyName = "Id" },
                new DataGridViewTextBoxColumn { Name = "PedidoId", HeaderText = "ID Pedido", DataPropertyName = "PedidoId" },
                new DataGridViewTextBoxColumn { Name = "ProductoId", HeaderText = "ID Producto", DataPropertyName = "ProductoId" },
                new DataGridViewTextBoxColumn { Name = "Cantidad", HeaderText = "Cantidad", DataPropertyName = "Cantidad" },
                new DataGridViewTextBoxColumn { Name = "PrecioUnitario", HeaderText = "Precio Unitario", DataPropertyName = "PrecioUnitario" }
            };

            dgvDetallePedido.Columns.AddRange(columns);

            // Evento de selección
            dgvDetallePedido.SelectionChanged += (s, e) =>
            {
                if (dgvDetallePedido.SelectedRows.Count > 0)
                {
                    var fila = dgvDetallePedido.SelectedRows[0].DataBoundItem as DetalleDto;
                    if (fila != null)
                    {
                        txtId.Text = fila.Id.ToString();
                        txtPedidoId.Text = fila.PedidoId.ToString();
                        txtProductoId.Text = fila.ProductoId.ToString();
                        txtCantidad.Text = fila.Cantidad.ToString();
                        txtPrecio.Text = fila.PrecioUnitario.ToString("N2");
                    }
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

        private async void btnCargar_Click(object sender, EventArgs e)
        {
            try
            {
                btnCargar.Enabled = false;
                Cursor = Cursors.WaitCursor;

                var response = await _repository.GetAllAsync();

                if (response.Success)
                {
                    dgvDetallePedido.DataSource = response.Data?.ToList();
                }
                else
                {
                    MessageBox.Show(response.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnCargar.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private async void btnCrear_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            try
            {
                var nuevo = new SaveDto
                {
                    PedidoId = int.Parse(txtPedidoId.Text),
                    ProductoId = int.Parse(txtProductoId.Text),
                    Cantidad = int.Parse(txtCantidad.Text),
                    PrecioUnitario = decimal.Parse(txtPrecio.Text)
                };

                var response = await _repository.CreateAsync(nuevo);

                if (response.Success)
                {
                    MessageBox.Show("Registro creado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await RecargarDatos();
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

        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos() || string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Seleccione un registro y complete los campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var actualizado = new UpdateDto
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
                    MessageBox.Show("Registro actualizado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await RecargarDatos();
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

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Seleccione un registro", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Eliminar este registro?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    var response = await _repository.DeleteAsync(int.Parse(txtId.Text));

                    if (response.Success)
                    {
                        MessageBox.Show("Registro eliminado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await RecargarDatos();
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
        }

        private async Task RecargarDatos()
        {
            var response = await _repository.GetAllAsync();
            if (response.Success)
            {
                dgvDetallePedido.DataSource = response.Data?.ToList();
                LimpiarFormulario();
            }
        }

        private bool ValidarCampos()
        {
            if (!int.TryParse(txtPedidoId.Text, out _) ||
                !int.TryParse(txtProductoId.Text, out _) ||
                !int.TryParse(txtCantidad.Text, out _) ||
                !decimal.TryParse(txtPrecio.Text, out _))
            {
                MessageBox.Show("Ingrese valores válidos", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
    }
}

