namespace SellPoint.View
{
    partial class FormProducto
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.TextBox txtPrecio;
        private System.Windows.Forms.TextBox txtStock;
        private System.Windows.Forms.Button btnCargar;
        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.GroupBox grpDatos;
        private System.Windows.Forms.FlowLayoutPanel pnlBotones;
        private System.Windows.Forms.ToolTip toolTip;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            dgvProductos = new System.Windows.Forms.DataGridView();
            txtId = new System.Windows.Forms.TextBox();
            txtNombre = new System.Windows.Forms.TextBox();
            txtDescripcion = new System.Windows.Forms.TextBox();
            txtPrecio = new System.Windows.Forms.TextBox();
            txtStock = new System.Windows.Forms.TextBox();
            btnCargar = new System.Windows.Forms.Button();
            btnCrear = new System.Windows.Forms.Button();
            btnActualizar = new System.Windows.Forms.Button();
            btnEliminar = new System.Windows.Forms.Button();
            lblId = new System.Windows.Forms.Label();
            lblNombre = new System.Windows.Forms.Label();
            lblDescripcion = new System.Windows.Forms.Label();
            lblPrecio = new System.Windows.Forms.Label();
            lblStock = new System.Windows.Forms.Label();
            grpDatos = new System.Windows.Forms.GroupBox();
            pnlBotones = new System.Windows.Forms.FlowLayoutPanel();
            toolTip = new System.Windows.Forms.ToolTip(components);

            ((System.ComponentModel.ISupportInitialize)(dgvProductos)).BeginInit();
            grpDatos.SuspendLayout();
            SuspendLayout();

            // dgvProductos
            dgvProductos.Location = new System.Drawing.Point(20, 210);
            dgvProductos.Size = new System.Drawing.Size(600, 200);
            dgvProductos.TabIndex = 0;

            // TextBoxes
            txtId.Location = new System.Drawing.Point(120, 22);
            txtId.Size = new System.Drawing.Size(280, 23);

            txtNombre.Location = new System.Drawing.Point(120, 52);
            txtNombre.Size = new System.Drawing.Size(280, 23);

            txtDescripcion.Location = new System.Drawing.Point(120, 82);
            txtDescripcion.Size = new System.Drawing.Size(280, 23);

            txtPrecio.Location = new System.Drawing.Point(120, 112);
            txtPrecio.Size = new System.Drawing.Size(280, 23);

            txtStock.Location = new System.Drawing.Point(120, 142);
            txtStock.Size = new System.Drawing.Size(280, 23);

            // Labels
            lblId.Location = new System.Drawing.Point(20, 25);
            lblId.Size = new System.Drawing.Size(100, 15);
            lblId.Text = "ID Producto:";

            lblNombre.Location = new System.Drawing.Point(20, 55);
            lblNombre.Size = new System.Drawing.Size(100, 15);
            lblNombre.Text = "Nombre:";

            lblDescripcion.Location = new System.Drawing.Point(20, 85);
            lblDescripcion.Size = new System.Drawing.Size(100, 15);
            lblDescripcion.Text = "Descripción:";

            lblPrecio.Location = new System.Drawing.Point(20, 115);
            lblPrecio.Size = new System.Drawing.Size(100, 15);
            lblPrecio.Text = "Precio:";

            lblStock.Location = new System.Drawing.Point(20, 145);
            lblStock.Size = new System.Drawing.Size(100, 15);
            lblStock.Text = "Stock:";

            // grpDatos
            grpDatos.Text = "Datos del Producto";
            grpDatos.Location = new System.Drawing.Point(20, 20);
            grpDatos.Size = new System.Drawing.Size(420, 180);
            grpDatos.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblId, txtId,
                lblNombre, txtNombre,
                lblDescripcion, txtDescripcion,
                lblPrecio, txtPrecio,
                lblStock, txtStock
            });

            // pnlBotones
            pnlBotones.Location = new System.Drawing.Point(460, 20);
            pnlBotones.Size = new System.Drawing.Size(160, 180);
            pnlBotones.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            pnlBotones.Controls.AddRange(new System.Windows.Forms.Control[] {
                btnCargar, btnCrear, btnActualizar, btnEliminar
            });

            // Botones
            ConfigurarBoton(btnCargar, "Cargar", "Cargar productos");
            ConfigurarBoton(btnCrear, "Crear", "Agregar nuevo producto");
            ConfigurarBoton(btnActualizar, "Actualizar", "Actualizar producto");
            ConfigurarBoton(btnEliminar, "Eliminar", "Eliminar producto");

            // Eventos
            btnCargar.Click += btnCargar_Click;
            btnCrear.Click += btnCrear_Click;
            btnActualizar.Click += btnActualizar_Click;
            btnEliminar.Click += btnEliminar_Click;

            // FormProducto
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(650, 430);
            Controls.AddRange(new System.Windows.Forms.Control[] {
                grpDatos, pnlBotones, dgvProductos
            });
            Name = "FormProducto";
            Text = "Gestión de Productos";
            Load += FormProducto_Load;

            ((System.ComponentModel.ISupportInitialize)(dgvProductos)).EndInit();
            grpDatos.ResumeLayout(false);
            grpDatos.PerformLayout();
            ResumeLayout(false);
        }

        private void ConfigurarBoton(Button btn, string texto, string tooltip)
        {
            btn.Size = new System.Drawing.Size(130, 28);
            btn.Text = texto;
            toolTip.SetToolTip(btn, tooltip);
        }
    }
}