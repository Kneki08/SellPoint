using System.Runtime.Versioning;
using SellPoint.Aplication.Dtos.Categoria;
using SellPoint.View.Models.ModelsCategoria;
using SellPoint.View.Services.CategoriaApiClient;

namespace SellPoint.View
{
    [SupportedOSPlatform("windows")]
    public partial class FormCategoria : Form
    {
        private readonly ICategoriaApiClient _categoriaApiClient;
        private readonly ErrorProvider _errorProvider = new();

        public FormCategoria(ICategoriaApiClient categoriaApiClient)
        {
            InitializeComponent();
            _categoriaApiClient = categoriaApiClient;

            // Opcional: configurar tooltips si deseas
            ConfigurarBoton(btnCargar, "Cargar", "Cargar categorías desde la base de datos");
            ConfigurarBoton(btnAgregar, "Crear", "Crear nueva categoría");
            ConfigurarBoton(btnActualizar, "Actualizar", "Actualizar categoría");
            ConfigurarBoton(btnEliminar, "Eliminar", "Eliminar categoría");
            ConfigurarBoton(btnLimpiar, "Limpiar", "Limpiar formulario");

            Load += FormCategoria_Load;
            dgvCategorias.CellClick += dgvCategorias_CellClick;
            btnCargar.Click += btnCargar_Click;
            btnAgregar.Click += btnAgregar_Click;
            btnActualizar.Click += btnActualizar_Click;
            btnEliminar.Click += btnEliminar_Click;
            btnLimpiar.Click += btnLimpiar_Click;
        }

        private void ConfigurarBoton(Button btn, string texto, string tooltipText)
        {
            toolTip.SetToolTip(btn, tooltipText);
        }

        private async void FormCategoria_Load(object? sender, EventArgs e) => await CargarCategoriasAsync();

        private async void btnCargar_Click(object? sender, EventArgs e) => await CargarCategoriasAsync();

        private async void btnAgregar_Click(object? sender, EventArgs e)
        {
            if (!ValidarFormulario()) return;

            var dto = new SaveCategoriaDTO
            {
                Nombre = txtNombre.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim(),
                Activo = chkActivo.Checked,
                EstaEliminado = chkEliminado.Checked
            };

            await ProcesarResultadoAsync(await _categoriaApiClient.CrearAsync(dto), "creada");
        }

        private async void btnActualizar_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("ID inválido.");
                return;
            }

            if (!ValidarFormulario()) return;

            var dto = new UpdateCategoriaDTO
            {
                Id = id,
                Nombre = txtNombre.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim(),
                Activo = chkActivo.Checked,
                EstaEliminado = chkEliminado.Checked
            };

            await ProcesarResultadoAsync(await _categoriaApiClient.ActualizarAsync(dto), "actualizada");
        }

        private async void btnEliminar_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("ID inválido.");
                return;
            }

            await ProcesarResultadoAsync(await _categoriaApiClient.EliminarAsync(new RemoveCategoriaDTO { Id = id }), "eliminada");
        }

        private void btnLimpiar_Click(object? sender, EventArgs e) => LimpiarFormulario();

        private async Task CargarCategoriasAsync()
        {
            try
            {
                var categorias = await _categoriaApiClient.ObtenerTodosAsync();
                dgvCategorias.DataSource = categorias.Select(dto => new CategoriaModel
                {
                    Id = dto.Id,
                    Nombre = dto.Nombre,
                    Descripcion = dto.Descripcion,
                    Activo = dto.Activo,
                    EstaEliminado = dto.EstaEliminado,
                    FechaCreacion = dto.FechaCreacion,
                    FechaActualizacion = dto.FechaActualizacion
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar categorías: " + ex.Message);
            }
        }

        private async Task ProcesarResultadoAsync(bool resultado, string accion)
        {
            if (resultado)
            {
                MessageBox.Show($"Categoría {accion} con éxito.");
                await CargarCategoriasAsync();
                LimpiarFormulario();
            }
            else
            {
                MessageBox.Show($"Error al {accion} la categoría.");
            }
        }

        private bool ValidarFormulario()
        {
            _errorProvider.Clear();
            bool valido = true;

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                _errorProvider.SetError(txtNombre, "El nombre es obligatorio.");
                valido = false;
            }

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                _errorProvider.SetError(txtDescripcion, "La descripción es obligatoria.");
                valido = false;
            }

            return valido;
        }

        private void LimpiarFormulario()
        {
            txtId.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            chkActivo.Checked = true;
            chkEliminado.Checked = false;
            dtpFechaCreacion.Value = DateTime.Now;
            dtpFechaActualizacion.Value = DateTime.Now;
            _errorProvider.Clear();
        }

        private void dgvCategorias_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var fila = dgvCategorias.Rows[e.RowIndex];

                txtId.Text = fila.Cells["Id"].Value?.ToString();
                txtNombre.Text = fila.Cells["Nombre"].Value?.ToString();
                txtDescripcion.Text = fila.Cells["Descripcion"].Value?.ToString();
                chkActivo.Checked = Convert.ToBoolean(fila.Cells["Activo"].Value);
                chkEliminado.Checked = Convert.ToBoolean(fila.Cells["EstaEliminado"].Value);

                // Fecha de creación
                if (DateTime.TryParse(fila.Cells["FechaCreacion"].Value?.ToString(), out var fechaCreacion) &&
                    fechaCreacion >= dtpFechaCreacion.MinDate && fechaCreacion <= dtpFechaCreacion.MaxDate)
                {
                    dtpFechaCreacion.Value = fechaCreacion;
                }
                else
                {
                    dtpFechaCreacion.Value = DateTime.Now;
                }

                // Fecha de actualización
                if (DateTime.TryParse(fila.Cells["FechaActualizacion"].Value?.ToString(), out var fechaActualizacion) &&
                    fechaActualizacion >= dtpFechaActualizacion.MinDate && fechaActualizacion <= dtpFechaActualizacion.MaxDate)
                {
                    dtpFechaActualizacion.Value = fechaActualizacion;
                }
                else
                {
                    dtpFechaActualizacion.Value = DateTime.Now;
                }
            }
        }
    }

}