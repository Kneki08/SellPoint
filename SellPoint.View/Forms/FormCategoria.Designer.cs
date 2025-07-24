namespace SellPoint.View
{
    partial class FormCategoria
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvCategorias;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Button btnCargar;
        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnEliminar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            dgvCategorias = new DataGridView();
            txtId = new TextBox();
            txtNombre = new TextBox();
            txtDescripcion = new TextBox();
            lblId = new Label();
            lblNombre = new Label();
            lblDescripcion = new Label();
            btnCargar = new Button();
            btnCrear = new Button();
            btnActualizar = new Button();
            btnEliminar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvCategorias).BeginInit();
            SuspendLayout();
            // 
            // dgvCategorias
            // 
            dgvCategorias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCategorias.Location = new Point(20, 200);
            dgvCategorias.Name = "dgvCategorias";
            dgvCategorias.RowHeadersWidth = 51;
            dgvCategorias.Size = new Size(600, 200);
            dgvCategorias.TabIndex = 0;
            // 
            // txtId
            // 
            txtId.Location = new Point(140, 20);
            txtId.Name = "txtId";
            txtId.Size = new Size(150, 27);
            txtId.TabIndex = 1;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(140, 60);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(150, 27);
            txtNombre.TabIndex = 2;
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(140, 100);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(150, 27);
            txtDescripcion.TabIndex = 3;
            // 
            // lblId
            // 
            lblId.AutoSize = true;
            lblId.Location = new Point(20, 23);
            lblId.Name = "lblId";
            lblId.Size = new Size(27, 20);
            lblId.TabIndex = 4;
            lblId.Text = "ID:";
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(20, 63);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(67, 20);
            lblNombre.TabIndex = 5;
            lblNombre.Text = "Nombre:";
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Location = new Point(20, 103);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(90, 20);
            lblDescripcion.TabIndex = 6;
            lblDescripcion.Text = "Descripción:";
            // 
            // btnCargar
            // 
            btnCargar.Location = new Point(320, 20);
            btnCargar.Name = "btnCargar";
            btnCargar.Size = new Size(100, 25);
            btnCargar.TabIndex = 7;
            btnCargar.Text = "Cargar";
            btnCargar.UseVisualStyleBackColor = true;
            btnCargar.Click += btnCargar_Click;
            // 
            // btnCrear
            // 
            btnCrear.Location = new Point(320, 60);
            btnCrear.Name = "btnCrear";
            btnCrear.Size = new Size(100, 25);
            btnCrear.TabIndex = 8;
            btnCrear.Text = "Crear";
            btnCrear.UseVisualStyleBackColor = true;
            btnCrear.Click += btnCrear_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(320, 100);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(100, 25);
            btnActualizar.TabIndex = 9;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(320, 140);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(100, 25);
            btnEliminar.TabIndex = 10;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // FormCategoria
            // 
            ClientSize = new Size(775, 430);
            Controls.Add(dgvCategorias);
            Controls.Add(txtId);
            Controls.Add(txtNombre);
            Controls.Add(txtDescripcion);
            Controls.Add(lblId);
            Controls.Add(lblNombre);
            Controls.Add(lblDescripcion);
            Controls.Add(btnCargar);
            Controls.Add(btnCrear);
            Controls.Add(btnActualizar);
            Controls.Add(btnEliminar);
            Name = "FormCategoria";
            Text = "Gestión de Categorías";
            Load += FormCategoria_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCategorias).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
