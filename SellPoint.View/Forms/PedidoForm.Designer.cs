namespace SellPoint.View.Forms
{
    partial class PedidoForm
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
            dgvPedidos = new DataGridView();
            groupBoxDatos = new GroupBox();
            labelUsuario = new Label();
            cmbUsuarios = new ComboBox();
            labelDireccion = new Label();
            cmbDirecciones = new ComboBox();
            labelMetodoPago = new Label();
            cmbMetodoPago = new ComboBox();
            labelReferencia = new Label();
            txtReferencia = new TextBox();
            labelSubtotal = new Label();
            txtSubtotal = new TextBox();
            labelDescuento = new Label();
            txtDescuento = new TextBox();
            labelCostoEnvio = new Label();
            txtCostoEnvio = new TextBox();
            labelTotal = new Label();
            txtTotal = new TextBox();
            labelEstado = new Label();
            cmbEstado = new ComboBox();
            labelNotas = new Label();
            txtNotas = new TextBox();
            btnAgregar = new Button();
            btnActualizar = new Button();
            btnEliminar = new Button();
            btnCargar = new Button();
            btnLimpiar = new Button();
            panelBotones = new Panel();
            lblBuscarId = new Label();
            txtBuscarId = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvPedidos).BeginInit();
            groupBoxDatos.SuspendLayout();
            SuspendLayout();
            // 
            // dgvPedidos
            // 
            dgvPedidos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvPedidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvPedidos.BackgroundColor = Color.White;
            dgvPedidos.ColumnHeadersHeight = 29;
            dgvPedidos.Location = new Point(30, 360);
            dgvPedidos.MultiSelect = false;
            dgvPedidos.Name = "dgvPedidos";
            dgvPedidos.ReadOnly = true;
            dgvPedidos.RowHeadersWidth = 51;
            dgvPedidos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPedidos.Size = new Size(1470, 500);
            dgvPedidos.TabIndex = 2;
            dgvPedidos.SelectionChanged += dgvPedidos_SelectionChanged;
            // 
            // groupBoxDatos
            // 
            groupBoxDatos.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxDatos.BackColor = Color.FromArgb(57, 62, 70);
            groupBoxDatos.Controls.Add(labelUsuario);
            groupBoxDatos.Controls.Add(cmbUsuarios);
            groupBoxDatos.Controls.Add(labelDireccion);
            groupBoxDatos.Controls.Add(cmbDirecciones);
            groupBoxDatos.Controls.Add(labelMetodoPago);
            groupBoxDatos.Controls.Add(cmbMetodoPago);
            groupBoxDatos.Controls.Add(labelReferencia);
            groupBoxDatos.Controls.Add(txtReferencia);
            groupBoxDatos.Controls.Add(labelSubtotal);
            groupBoxDatos.Controls.Add(txtSubtotal);
            groupBoxDatos.Controls.Add(labelDescuento);
            groupBoxDatos.Controls.Add(txtDescuento);
            groupBoxDatos.Controls.Add(labelCostoEnvio);
            groupBoxDatos.Controls.Add(txtCostoEnvio);
            groupBoxDatos.Controls.Add(labelTotal);
            groupBoxDatos.Controls.Add(txtTotal);
            groupBoxDatos.Controls.Add(labelEstado);
            groupBoxDatos.Controls.Add(cmbEstado);
            groupBoxDatos.Controls.Add(labelNotas);
            groupBoxDatos.Controls.Add(txtNotas);
            groupBoxDatos.ForeColor = Color.White;
            groupBoxDatos.Location = new Point(30, 20);
            groupBoxDatos.Name = "groupBoxDatos";
            groupBoxDatos.Size = new Size(1470, 260);
            groupBoxDatos.TabIndex = 0;
            groupBoxDatos.TabStop = false;
            groupBoxDatos.Text = "Datos del Pedido";
            // 
            // labelUsuario
            // 
            labelUsuario.ForeColor = Color.White;
            labelUsuario.Location = new Point(30, 30);
            labelUsuario.Name = "labelUsuario";
            labelUsuario.Size = new Size(150, 25);
            labelUsuario.TabIndex = 0;
            labelUsuario.Text = "ID Usuario:";
            //
            // cmbUsuarios
            //
            cmbUsuarios.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUsuarios.Location = new Point(200, 30);
            cmbUsuarios.Name = "cmbUsuarios";
            cmbUsuarios.Size = new Size(180, 30);
            cmbUsuarios.TabIndex = 1;

            // 
            // labelDireccion
            // 
            labelDireccion.ForeColor = Color.White;
            labelDireccion.Location = new Point(30, 65);
            labelDireccion.Name = "labelDireccion";
            labelDireccion.Size = new Size(150, 25);
            labelDireccion.TabIndex = 2;
            labelDireccion.Text = "ID Dirección:";
            // 
            // cmbDirecciones
            // 
            cmbDirecciones.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDirecciones.Location = new Point(200, 65);
            cmbDirecciones.Name = "cmbDirecciones";
            cmbDirecciones.Size = new Size(180, 30);
            cmbDirecciones.TabIndex = 3;
            // 
            // labelMetodoPago
            // 
            labelMetodoPago.ForeColor = Color.White;
            labelMetodoPago.Location = new Point(30, 100);
            labelMetodoPago.Name = "labelMetodoPago";
            labelMetodoPago.Size = new Size(150, 25);
            labelMetodoPago.TabIndex = 4;
            labelMetodoPago.Text = "Método de Pago:";
            // 
            // cmbMetodoPago
            // 
            cmbMetodoPago.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMetodoPago.Location = new Point(200, 100);
            cmbMetodoPago.Name = "cmbMetodoPago";
            cmbMetodoPago.Size = new Size(180, 30);
            cmbMetodoPago.TabIndex = 5;
            // 
            // labelReferencia
            // 
            labelReferencia.ForeColor = Color.White;
            labelReferencia.Location = new Point(30, 135);
            labelReferencia.Name = "labelReferencia";
            labelReferencia.Size = new Size(150, 25);
            labelReferencia.TabIndex = 6;
            labelReferencia.Text = "Referencia Pago:";
            // 
            // txtReferencia
            // 
            txtReferencia.Location = new Point(200, 135);
            txtReferencia.Name = "txtReferencia";
            txtReferencia.Size = new Size(180, 30);
            txtReferencia.TabIndex = 7;
            // 
            // labelSubtotal
            // 
            labelSubtotal.ForeColor = Color.White;
            labelSubtotal.Location = new Point(450, 30);
            labelSubtotal.Name = "labelSubtotal";
            labelSubtotal.Size = new Size(150, 25);
            labelSubtotal.TabIndex = 8;
            labelSubtotal.Text = "Subtotal:";
            // 
            // txtSubtotal
            // 
            txtSubtotal.Location = new Point(630, 30);
            txtSubtotal.Name = "txtSubtotal";
            txtSubtotal.Size = new Size(180, 30);
            txtSubtotal.TabIndex = 9;
            // 
            // labelDescuento
            // 
            labelDescuento.ForeColor = Color.White;
            labelDescuento.Location = new Point(450, 65);
            labelDescuento.Name = "labelDescuento";
            labelDescuento.Size = new Size(150, 25);
            labelDescuento.TabIndex = 10;
            labelDescuento.Text = "Descuento:";
            // 
            // txtDescuento
            // 
            txtDescuento.Location = new Point(630, 65);
            txtDescuento.Name = "txtDescuento";
            txtDescuento.Size = new Size(180, 30);
            txtDescuento.TabIndex = 11;
            // 
            // labelCostoEnvio
            // 
            labelCostoEnvio.ForeColor = Color.White;
            labelCostoEnvio.Location = new Point(450, 100);
            labelCostoEnvio.Name = "labelCostoEnvio";
            labelCostoEnvio.Size = new Size(150, 25);
            labelCostoEnvio.TabIndex = 12;
            labelCostoEnvio.Text = "Costo Envío:";
            // 
            // txtCostoEnvio
            // 
            txtCostoEnvio.Location = new Point(630, 100);
            txtCostoEnvio.Name = "txtCostoEnvio";
            txtCostoEnvio.Size = new Size(180, 30);
            txtCostoEnvio.TabIndex = 13;
            // 
            // labelTotal
            // 
            labelTotal.ForeColor = Color.White;
            labelTotal.Location = new Point(450, 135);
            labelTotal.Name = "labelTotal";
            labelTotal.Size = new Size(150, 25);
            labelTotal.TabIndex = 14;
            labelTotal.Text = "Total:";
            // 
            // txtTotal
            // 
            txtTotal.Location = new Point(630, 135);
            txtTotal.Name = "txtTotal";
            txtTotal.Size = new Size(180, 30);
            txtTotal.TabIndex = 15;
            // 
            // labelEstado
            // 
            labelEstado.ForeColor = Color.White;
            labelEstado.Location = new Point(870, 30);
            labelEstado.Name = "labelEstado";
            labelEstado.Size = new Size(150, 25);
            labelEstado.TabIndex = 16;
            labelEstado.Text = "Estado:";
            // 
            // cmbEstado
            // 
            cmbEstado.Location = new Point(1050, 30);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(180, 31);
            cmbEstado.TabIndex = 17;
            // 
            // labelNotas
            // 
            labelNotas.ForeColor = Color.White;
            labelNotas.Location = new Point(870, 65);
            labelNotas.Name = "labelNotas";
            labelNotas.Size = new Size(150, 25);
            labelNotas.TabIndex = 18;
            labelNotas.Text = "Notas:";
            // 
            // txtNotas
            // 
            txtNotas.Location = new Point(1050, 65);
            txtNotas.Multiline = true;
            txtNotas.Name = "txtNotas";
            txtNotas.Size = new Size(350, 31);
            txtNotas.TabIndex = 19;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.LightGreen;
            btnAgregar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAgregar.Location = new Point(365, 290);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(140, 50);
            btnAgregar.TabIndex = 0;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.BackColor = Color.Khaki;
            btnActualizar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnActualizar.Location = new Point(523, 290);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(140, 50);
            btnActualizar.TabIndex = 1;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = false;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.LightCoral;
            btnEliminar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnEliminar.Location = new Point(683, 290);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(140, 50);
            btnEliminar.TabIndex = 2;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnCargar
            // 
            btnCargar.BackColor = Color.LightBlue;
            btnCargar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCargar.Location = new Point(843, 290);
            btnCargar.Name = "btnCargar";
            btnCargar.Size = new Size(140, 50);
            btnCargar.TabIndex = 3;
            btnCargar.Text = "Cargar";
            btnCargar.UseVisualStyleBackColor = false;
            btnCargar.Click += btnCargar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.BackColor = Color.Silver;
            btnLimpiar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLimpiar.Location = new Point(1003, 290);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(140, 50);
            btnLimpiar.TabIndex = 4;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = false;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // panelBotones
            // 
            panelBotones.Location = new Point(0, 0);
            panelBotones.Name = "panelBotones";
            panelBotones.Size = new Size(200, 100);
            panelBotones.TabIndex = 0;
            // 
            // lblBuscarId
            // 
            lblBuscarId.ForeColor = Color.White;
            lblBuscarId.Location = new Point(50, 300);
            lblBuscarId.Name = "lblBuscarId";
            lblBuscarId.Size = new Size(100, 30);
            lblBuscarId.TabIndex = 5;
            lblBuscarId.Text = "ID a buscar:";
            // 
            // txtBuscarId
            // 
            txtBuscarId.Location = new Point(160, 300);
            txtBuscarId.Name = "txtBuscarId";
            txtBuscarId.Size = new Size(120, 30);
            txtBuscarId.TabIndex = 6;
            // 
            // PedidoForm
            // 
            BackColor = Color.FromArgb(34, 40, 49);
            ClientSize = new Size(1550, 900);
            Controls.Add(groupBoxDatos);
            Controls.Add(btnAgregar);
            Controls.Add(btnActualizar);
            Controls.Add(btnEliminar);
            Controls.Add(btnCargar);
            Controls.Add(btnLimpiar);
            Controls.Add(dgvPedidos);
            Controls.Add(lblBuscarId);
            Controls.Add(txtBuscarId);
            Font = new Font("Segoe UI", 10F);
            Name = "PedidoForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Pedidos";
            ((System.ComponentModel.ISupportInitialize)dgvPedidos).EndInit();
            groupBoxDatos.ResumeLayout(false);
            groupBoxDatos.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private void AgregarCampo(Control contenedor, Label label, string texto, Control control, int xLabel, int xControl, int y)
        {
            label.Text = texto;
            label.Location = new Point(xLabel, y);
            label.Size = new Size(150, 25);
            control.Location = new Point(xControl, y);
            control.Size = new Size(180, 27);
            contenedor.Controls.Add(label);
            contenedor.Controls.Add(control);
        }

        #endregion
        public ComboBox cmbUsuarios, cmbDirecciones, cmbMetodoPago, cmbEstado;
        public TextBox txtReferencia, txtSubtotal, txtDescuento, txtCostoEnvio, txtTotal, txtNotas;
        public Label labelUsuario, labelDireccion, labelMetodoPago, labelReferencia, labelSubtotal,
            labelDescuento, labelCostoEnvio, labelTotal, labelNotas, labelEstado;
        public Button btnAgregar, btnActualizar, btnEliminar, btnCargar, btnLimpiar;
        public Panel panelBotones;
        public TextBox txtBuscarId;
        public Label lblBuscarId;
        public DataGridView dgvPedidos;
        public GroupBox groupBoxDatos;
    }
}
