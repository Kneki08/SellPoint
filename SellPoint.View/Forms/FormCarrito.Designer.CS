namespace SellPoint.View
{
    partial class FormCarrito : System.Windows.Forms.Form // Ensure FormCarrito inherits from System.Windows.Forms.Form
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvCarritos;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtClienteId;
        private System.Windows.Forms.DateTimePicker dtpFechaCreacion;
        private System.Windows.Forms.TextBox txtEstado;
        private System.Windows.Forms.Button btnCargar;
        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblClienteId;
        private System.Windows.Forms.Label lblFechaCreacion;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.GroupBox grpDatos;
        private System.Windows.Forms.FlowLayoutPanel pnlBotones;
        private System.Windows.Forms.ToolTip toolTip;

        protected override void Dispose(bool disposing) // Ensure Dispose method matches the base class signature
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            dgvCarritos = new System.Windows.Forms.DataGridView();
            txtId = new System.Windows.Forms.TextBox();
            txtClienteId = new System.Windows.Forms.TextBox();
            dtpFechaCreacion = new System.Windows.Forms.DateTimePicker();
            txtEstado = new System.Windows.Forms.TextBox();
            btnCargar = new System.Windows.Forms.Button();
            btnCrear = new System.Windows.Forms.Button();
            btnActualizar = new System.Windows.Forms.Button();
            btnEliminar = new System.Windows.Forms.Button();
            lblId = new System.Windows.Forms.Label();
            lblClienteId = new System.Windows.Forms.Label();
            lblFechaCreacion = new System.Windows.Forms.Label();
            lblEstado = new System.Windows.Forms.Label();
            grpDatos = new System.Windows.Forms.GroupBox();
            pnlBotones = new System.Windows.Forms.FlowLayoutPanel();
            toolTip = new System.Windows.Forms.ToolTip(components);

            ((System.ComponentModel.ISupportInitialize)(dgvCarritos)).BeginInit();
            grpDatos.SuspendLayout();
            SuspendLayout();

            // dgvCarritos
            dgvCarritos.Location = new System.Drawing.Point(20, 210);
            dgvCarritos.Size = new System.Drawing.Size(600, 200);
            dgvCarritos.TabIndex = 0;

            // txtId
            txtId.Location = new System.Drawing.Point(120, 22);
            txtId.Size = new System.Drawing.Size(280, 23);

            // txtClienteId
            txtClienteId.Location = new System.Drawing.Point(120, 52);
            txtClienteId.Size = new System.Drawing.Size(280, 23);

            // dtpFechaCreacion
            dtpFechaCreacion.Location = new System.Drawing.Point(120, 82);
            dtpFechaCreacion.Size = new System.Drawing.Size(280, 23);

            // txtEstado
            txtEstado.Location = new System.Drawing.Point(120, 112);
            txtEstado.Size = new System.Drawing.Size(280, 23);

            // Labels
            lblId.Location = new System.Drawing.Point(20, 25);
            lblId.Size = new System.Drawing.Size(100, 15);
            lblId.Text = "ID Carrito:";

            lblClienteId.Location = new System.Drawing.Point(20, 55);
            lblClienteId.Size = new System.Drawing.Size(100, 15);
            lblClienteId.Text = "Cliente ID:";

            lblFechaCreacion.Location = new System.Drawing.Point(20, 85);
            lblFechaCreacion.Size = new System.Drawing.Size(100, 15);
            lblFechaCreacion.Text = "Fecha Creación:";

            lblEstado.Location = new System.Drawing.Point(20, 115);
            lblEstado.Size = new System.Drawing.Size(100, 15);
            lblEstado.Text = "Estado:";

            // grpDatos
            grpDatos.Text = "Datos del Carrito";
            grpDatos.Location = new System.Drawing.Point(20, 20);
            grpDatos.Size = new System.Drawing.Size(420, 150);
            grpDatos.Controls.AddRange(new System.Windows.Forms.Control[] {
                    lblId, txtId,
                    lblClienteId, txtClienteId,
                    lblFechaCreacion, dtpFechaCreacion,
                    lblEstado, txtEstado
                });

            // pnlBotones
            pnlBotones.Location = new System.Drawing.Point(460, 20);
            pnlBotones.Size = new System.Drawing.Size(160, 150);
            pnlBotones.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            pnlBotones.Controls.AddRange(new System.Windows.Forms.Control[] {
                    btnCargar, btnCrear, btnActualizar, btnEliminar
                });

            // Botones
            ConfigurarBoton(btnCargar, "Cargar", "Cargar carritos");
            ConfigurarBoton(btnCrear, "Crear", "Agregar nuevo carrito");
            ConfigurarBoton(btnActualizar, "Actualizar", "Actualizar carrito");
            ConfigurarBoton(btnEliminar, "Eliminar", "Eliminar carrito");

            // Eventos
            btnCargar.Click += btnCargar_Click;
            btnCrear.Click += btnCrear_Click;
            btnActualizar.Click += btnActualizar_Click;
            btnEliminar.Click += btnEliminar_Click;

            // FormCarrito
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(650, 430);
            Controls.AddRange(new System.Windows.Forms.Control[] {
                    grpDatos, pnlBotones, dgvCarritos
                });
            Name = "FormCarrito";
            Text = "Gestión de Carritos";
            Load += FormCarrito_Load;

            ((System.ComponentModel.ISupportInitialize)(dgvCarritos)).EndInit();
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
