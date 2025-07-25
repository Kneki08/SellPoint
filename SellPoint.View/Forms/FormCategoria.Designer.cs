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
            dgvCategorias = new DataGridView();
            txtId = new TextBox();
            txtNombre = new TextBox();
            txtDescripcion = new TextBox();
            btnCargar = new Button();
            btnCrear = new Button();
            btnActualizar = new Button();
            btnEliminar = new Button();
            lblId = new Label();
            lblNombre = new Label();
            lblDescripcion = new Label();
            grpDatos = new GroupBox();
            pnlBotones = new FlowLayoutPanel();
            toolTip = new ToolTip(components);

            ((System.ComponentModel.ISupportInitialize)(dgvCategorias)).BeginInit();
            SuspendLayout();

            // 
            // grpDatos
            // 
            grpDatos.Text = "Datos de Categoría";
            grpDatos.Location = new Point(20, 20);
            grpDatos.Size = new Size(440, 130);
            grpDatos.Controls.AddRange(new Control[] { lblId, txtId, lblNombre, txtNombre, lblDescripcion, txtDescripcion });

            // 
            // lblId
            // 
            lblId.Location = new Point(20, 25);
            lblId.Size = new Size(60, 15);
            lblId.Text = "ID:";

            txtId.Location = new Point(100, 22);
            txtId.Size = new Size(300, 23);

            // 
            // lblNombre
            // 
            lblNombre.Location = new Point(20, 55);
            lblNombre.Size = new Size(60, 15);
            lblNombre.Text = "Nombre:";

            txtNombre.Location = new Point(100, 52);
            txtNombre.Size = new Size(300, 23);

            // 
            // lblDescripcion
            // 
            lblDescripcion.Location = new Point(20, 85);
            lblDescripcion.Size = new Size(75, 15);
            lblDescripcion.Text = "Descripción:";

            txtDescripcion.Location = new Point(100, 82);
            txtDescripcion.Size = new Size(300, 23);

            // 
            // pnlBotones
            // 
            pnlBotones.Location = new Point(480, 20);
            pnlBotones.Size = new Size(140, 130);
            pnlBotones.FlowDirection = FlowDirection.TopDown;
            pnlBotones.Controls.AddRange(new Control[] { btnCargar, btnCrear, btnActualizar, btnEliminar });

            // Configuración de botones con ToolTip
            void ConfigurarBoton(Button btn, string texto, string tooltip)
            {
                btn.Size = new Size(120, 25);
                btn.Text = texto;
                toolTip.SetToolTip(btn, tooltip);
            }

            ConfigurarBoton(btnCargar, "Cargar", "Cargar todas las categorías");
            ConfigurarBoton(btnCrear, "Crear", "Crear nueva categoría");
            ConfigurarBoton(btnActualizar, "Actualizar", "Actualizar la categoría actual");
            ConfigurarBoton(btnEliminar, "Eliminar", "Eliminar la categoría seleccionada");

            btnCargar.Click += btnCargar_Click;
            btnCrear.Click += btnCrear_Click;
            btnActualizar.Click += btnActualizar_Click;
            btnEliminar.Click += btnEliminar_Click;

            // 
            // dgvCategorias
            // 
            dgvCategorias.Location = new Point(20, 170);
            dgvCategorias.Size = new Size(600, 200);

            // 
            // FormCategoria
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(650, 400);
            Controls.AddRange(new Control[] { grpDatos, pnlBotones, dgvCategorias });
            Name = "FormCategoria";
            Text = "Gestión de Categorías";
            Load += FormCategoria_Load;

            ((System.ComponentModel.ISupportInitialize)(dgvCategorias)).EndInit();
            ResumeLayout(false);
        }
    }
}
