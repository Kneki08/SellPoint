namespace SellPoint.View
{
    partial class FormCategoria
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvCategorias;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Button btnCargar;
        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblDescripcion;
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
            dgvCategorias = new System.Windows.Forms.DataGridView();
            txtId = new System.Windows.Forms.TextBox();
            txtNombre = new System.Windows.Forms.TextBox();
            txtDescripcion = new System.Windows.Forms.TextBox();
            btnCargar = new System.Windows.Forms.Button();
            btnCrear = new System.Windows.Forms.Button();
            btnActualizar = new System.Windows.Forms.Button();
            btnEliminar = new System.Windows.Forms.Button();
            lblId = new System.Windows.Forms.Label();
            lblNombre = new System.Windows.Forms.Label();
            lblDescripcion = new System.Windows.Forms.Label();
            grpDatos = new System.Windows.Forms.GroupBox();
            pnlBotones = new System.Windows.Forms.FlowLayoutPanel();
            toolTip = new System.Windows.Forms.ToolTip(components);

            ((System.ComponentModel.ISupportInitialize)(dgvCategorias)).BeginInit();
            grpDatos.SuspendLayout();
            SuspendLayout();

            // dgvCategorias
            dgvCategorias.Location = new System.Drawing.Point(20, 170);
            dgvCategorias.Size = new System.Drawing.Size(600, 200);
            dgvCategorias.TabIndex = 0;

            // txtId
            txtId.Location = new System.Drawing.Point(100, 22);
            txtId.Size = new System.Drawing.Size(300, 23);

            // txtNombre
            txtNombre.Location = new System.Drawing.Point(100, 52);
            txtNombre.Size = new System.Drawing.Size(300, 23);

            // txtDescripcion
            txtDescripcion.Location = new System.Drawing.Point(100, 82);
            txtDescripcion.Size = new System.Drawing.Size(300, 23);

            // lblId
            lblId.Location = new System.Drawing.Point(20, 25);
            lblId.Size = new System.Drawing.Size(60, 15);
            lblId.Text = "ID:";

            // lblNombre
            lblNombre.Location = new System.Drawing.Point(20, 55);
            lblNombre.Size = new System.Drawing.Size(60, 15);
            lblNombre.Text = "Nombre:";

            // lblDescripcion
            lblDescripcion.Location = new System.Drawing.Point(20, 85);
            lblDescripcion.Size = new System.Drawing.Size(75, 15);
            lblDescripcion.Text = "Descripción:";

            // grpDatos
            grpDatos.Text = "Datos de Categoría";
            grpDatos.Location = new System.Drawing.Point(20, 20);
            grpDatos.Size = new System.Drawing.Size(440, 130);
            grpDatos.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblId, txtId, lblNombre, txtNombre, lblDescripcion, txtDescripcion
            });

            // pnlBotones
            pnlBotones.Location = new System.Drawing.Point(480, 20);
            pnlBotones.Size = new System.Drawing.Size(140, 130);
            pnlBotones.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            pnlBotones.Controls.AddRange(new System.Windows.Forms.Control[] {
                btnCargar, btnCrear, btnActualizar, btnEliminar
            });

            // Botones
            ConfigurarBoton(btnCargar, "Cargar", "Cargar todas las categorías");
            ConfigurarBoton(btnCrear, "Crear", "Crear nueva categoría");
            ConfigurarBoton(btnActualizar, "Actualizar", "Actualizar la categoría actual");
            ConfigurarBoton(btnEliminar, "Eliminar", "Eliminar la categoría seleccionada");

            // Eventos
            btnCargar.Click += btnCargar_Click;
            btnCrear.Click += btnCrear_Click;
            btnActualizar.Click += btnActualizar_Click;
            btnEliminar.Click += btnEliminar_Click;

            // FormCategoria
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(650, 400);
            Controls.AddRange(new System.Windows.Forms.Control[] {
                grpDatos, pnlBotones, dgvCategorias
            });
            Name = "FormCategoria";
            Text = "Gestión de Categorías";
            Load += FormCategoria_Load;

            ((System.ComponentModel.ISupportInitialize)(dgvCategorias)).EndInit();
            grpDatos.ResumeLayout(false);
            grpDatos.PerformLayout();
            ResumeLayout(false);
        }

        private void ConfigurarBoton(Button btn, string texto, string tooltip)
        {
            btn.Size = new System.Drawing.Size(120, 25);
            btn.Text = texto;
            toolTip.SetToolTip(btn, tooltip);
        }
    }
}

