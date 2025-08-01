namespace SellPoint.View
{
    partial class FormCategoria
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            dgvCategorias = new DataGridView();
            groupBoxDatos = new GroupBox();
            labelId = new Label();
            txtId = new TextBox();
            labelNombre = new Label();
            txtNombre = new TextBox();
            labelDescripcion = new Label();
            txtDescripcion = new TextBox();
            labelActivo = new Label();
            chkActivo = new CheckBox();
            labelEliminado = new Label();
            chkEliminado = new CheckBox();
            labelFechaCreacion = new Label();
            dtpFechaCreacion = new DateTimePicker();
            labelFechaActualizacion = new Label();
            dtpFechaActualizacion = new DateTimePicker();
            btnAgregar = new Button();
            btnActualizar = new Button();
            btnEliminar = new Button();
            btnCargar = new Button();
            btnLimpiar = new Button();
            lblBuscarId = new Label();
            txtBuscarId = new TextBox();
            toolTip = new ToolTip(components);
            ((System.ComponentModel.ISupportInitialize)dgvCategorias).BeginInit();
            groupBoxDatos.SuspendLayout();
            SuspendLayout();
            // 
            // dgvCategorias
            // 
            dgvCategorias.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvCategorias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvCategorias.BackgroundColor = Color.White;
            dgvCategorias.ColumnHeadersHeight = 29;
            dgvCategorias.Location = new Point(30, 360);
            dgvCategorias.MultiSelect = false;
            dgvCategorias.Name = "dgvCategorias";
            dgvCategorias.ReadOnly = true;
            dgvCategorias.RowHeadersWidth = 51;
            dgvCategorias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCategorias.Size = new Size(1049, 300);
            dgvCategorias.TabIndex = 8;
            // 
            // groupBoxDatos
            // 
            groupBoxDatos.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxDatos.BackColor = Color.FromArgb(57, 62, 70);
            groupBoxDatos.Controls.Add(labelId);
            groupBoxDatos.Controls.Add(txtId);
            groupBoxDatos.Controls.Add(labelNombre);
            groupBoxDatos.Controls.Add(txtNombre);
            groupBoxDatos.Controls.Add(labelDescripcion);
            groupBoxDatos.Controls.Add(txtDescripcion);
            groupBoxDatos.Controls.Add(labelActivo);
            groupBoxDatos.Controls.Add(chkActivo);
            groupBoxDatos.Controls.Add(labelEliminado);
            groupBoxDatos.Controls.Add(chkEliminado);
            groupBoxDatos.Controls.Add(labelFechaCreacion);
            groupBoxDatos.Controls.Add(dtpFechaCreacion);
            groupBoxDatos.Controls.Add(labelFechaActualizacion);
            groupBoxDatos.Controls.Add(dtpFechaActualizacion);
            groupBoxDatos.ForeColor = Color.White;
            groupBoxDatos.Location = new Point(30, 20);
            groupBoxDatos.Name = "groupBoxDatos";
            groupBoxDatos.Size = new Size(1049, 200);
            groupBoxDatos.TabIndex = 0;
            groupBoxDatos.TabStop = false;
            groupBoxDatos.Text = "Datos de la Categoría";
            // 
            // labelId
            // 
            labelId.Location = new Point(20, 30);
            labelId.Name = "labelId";
            labelId.Size = new Size(120, 25);
            labelId.TabIndex = 0;
            labelId.Text = "ID:";
            // 
            // txtId
            // 
            txtId.Location = new Point(150, 30);
            txtId.Name = "txtId";
            txtId.Size = new Size(200, 30);
            txtId.TabIndex = 1;
            // 
            // labelNombre
            // 
            labelNombre.Location = new Point(400, 30);
            labelNombre.Name = "labelNombre";
            labelNombre.Size = new Size(120, 25);
            labelNombre.TabIndex = 2;
            labelNombre.Text = "Nombre:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(530, 30);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(200, 30);
            txtNombre.TabIndex = 3;
            // 
            // labelDescripcion
            // 
            labelDescripcion.Location = new Point(20, 70);
            labelDescripcion.Name = "labelDescripcion";
            labelDescripcion.Size = new Size(120, 25);
            labelDescripcion.TabIndex = 4;
            labelDescripcion.Text = "Descripción:";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(150, 70);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(580, 30);
            txtDescripcion.TabIndex = 5;
            // 
            // labelActivo
            // 
            labelActivo.Location = new Point(20, 110);
            labelActivo.Name = "labelActivo";
            labelActivo.Size = new Size(120, 25);
            labelActivo.TabIndex = 6;
            labelActivo.Text = "Activo:";
            // 
            // chkActivo
            // 
            chkActivo.Location = new Point(150, 110);
            chkActivo.Name = "chkActivo";
            chkActivo.Size = new Size(20, 20);
            chkActivo.TabIndex = 7;
            // 
            // labelEliminado
            // 
            labelEliminado.Location = new Point(200, 110);
            labelEliminado.Name = "labelEliminado";
            labelEliminado.Size = new Size(120, 25);
            labelEliminado.TabIndex = 8;
            labelEliminado.Text = "Eliminado:";
            // 
            // chkEliminado
            // 
            chkEliminado.Location = new Point(330, 110);
            chkEliminado.Name = "chkEliminado";
            chkEliminado.Size = new Size(20, 20);
            chkEliminado.TabIndex = 9;
            // 
            // labelFechaCreacion
            // 
            labelFechaCreacion.Location = new Point(20, 150);
            labelFechaCreacion.Name = "labelFechaCreacion";
            labelFechaCreacion.Size = new Size(120, 25);
            labelFechaCreacion.TabIndex = 10;
            labelFechaCreacion.Text = "Fecha Creación:";
            // 
            // dtpFechaCreacion
            // 
            dtpFechaCreacion.Location = new Point(150, 150);
            dtpFechaCreacion.Name = "dtpFechaCreacion";
            dtpFechaCreacion.Size = new Size(250, 30);
            dtpFechaCreacion.TabIndex = 11;
            // 
            // labelFechaActualizacion
            // 
            labelFechaActualizacion.Location = new Point(430, 150);
            labelFechaActualizacion.Name = "labelFechaActualizacion";
            labelFechaActualizacion.Size = new Size(150, 25);
            labelFechaActualizacion.TabIndex = 12;
            labelFechaActualizacion.Text = "Fecha Actualización:";
            // 
            // dtpFechaActualizacion
            // 
            dtpFechaActualizacion.Location = new Point(586, 150);
            dtpFechaActualizacion.Name = "dtpFechaActualizacion";
            dtpFechaActualizacion.Size = new Size(250, 30);
            dtpFechaActualizacion.TabIndex = 13;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.LightGreen;
            btnAgregar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAgregar.Location = new Point(180, 240);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(120, 45);
            btnAgregar.TabIndex = 3;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            // 
            // btnActualizar
            // 
            btnActualizar.BackColor = Color.Khaki;
            btnActualizar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnActualizar.Location = new Point(310, 240);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(120, 45);
            btnActualizar.TabIndex = 4;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = false;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.IndianRed;
            btnEliminar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnEliminar.Location = new Point(440, 240);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(120, 45);
            btnEliminar.TabIndex = 5;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            // 
            // btnCargar
            // 
            btnCargar.BackColor = Color.LightSkyBlue;
            btnCargar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCargar.Location = new Point(570, 240);
            btnCargar.Name = "btnCargar";
            btnCargar.Size = new Size(120, 45);
            btnCargar.TabIndex = 6;
            btnCargar.Text = "Cargar";
            btnCargar.UseVisualStyleBackColor = false;
            // 
            // btnLimpiar
            // 
            btnLimpiar.BackColor = Color.Silver;
            btnLimpiar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLimpiar.Location = new Point(700, 240);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(120, 45);
            btnLimpiar.TabIndex = 7;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = false;
            // 
            // lblBuscarId
            // 
            lblBuscarId.ForeColor = Color.White;
            lblBuscarId.Location = new Point(30, 250);
            lblBuscarId.Name = "lblBuscarId";
            lblBuscarId.Size = new Size(100, 25);
            lblBuscarId.TabIndex = 1;
            lblBuscarId.Text = "ID a buscar:";
            // 
            // txtBuscarId
            // 
            txtBuscarId.Location = new Point(130, 250);
            txtBuscarId.Name = "txtBuscarId";
            txtBuscarId.Size = new Size(40, 30);
            txtBuscarId.TabIndex = 2;
            // 
            // FormCategoria
            // 
            BackColor = Color.FromArgb(34, 40, 49);
            ClientSize = new Size(1109, 700);
            Controls.Add(groupBoxDatos);
            Controls.Add(lblBuscarId);
            Controls.Add(txtBuscarId);
            Controls.Add(btnAgregar);
            Controls.Add(btnActualizar);
            Controls.Add(btnEliminar);
            Controls.Add(btnCargar);
            Controls.Add(btnLimpiar);
            Controls.Add(dgvCategorias);
            Font = new Font("Segoe UI", 10F);
            Name = "FormCategoria";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Categorías";
            ((System.ComponentModel.ISupportInitialize)dgvCategorias).EndInit();
            groupBoxDatos.ResumeLayout(false);
            groupBoxDatos.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxDatos;
        private System.Windows.Forms.DataGridView dgvCategorias;
        private System.Windows.Forms.Label labelId, labelNombre, labelDescripcion, labelActivo, labelEliminado, labelFechaCreacion, labelFechaActualizacion;
        private System.Windows.Forms.TextBox txtId, txtNombre, txtDescripcion, txtBuscarId;
        private System.Windows.Forms.CheckBox chkActivo, chkEliminado;
        private System.Windows.Forms.DateTimePicker dtpFechaCreacion, dtpFechaActualizacion;
        private System.Windows.Forms.Label lblBuscarId;
        private System.Windows.Forms.Button btnAgregar, btnActualizar, btnEliminar, btnCargar, btnLimpiar;
        private System.Windows.Forms.ToolTip toolTip;
    }
}


