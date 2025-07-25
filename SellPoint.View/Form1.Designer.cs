namespace SellPoint.View
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvDetallePedido;
        private System.Windows.Forms.Button btnCargar;
        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnEliminar;

        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.TextBox txtPrecio;
        private System.Windows.Forms.TextBox txtPedidoId;
        private System.Windows.Forms.TextBox txtProductoId;

        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.Label lblPedidoId;
        private System.Windows.Forms.Label lblProductoId;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            dgvDetallePedido = new DataGridView();
            btnCargar = new Button();
            btnCrear = new Button();
            btnActualizar = new Button();
            btnEliminar = new Button();

            txtId = new TextBox();
            txtCantidad = new TextBox();
            txtPrecio = new TextBox();
            txtPedidoId = new TextBox();
            txtProductoId = new TextBox();

            lblId = new Label();
            lblCantidad = new Label();
            lblPrecio = new Label();
            lblPedidoId = new Label();
            lblProductoId = new Label();

            ((System.ComponentModel.ISupportInitialize)dgvDetallePedido).BeginInit();
            SuspendLayout();

            // Labels
            lblId.Text = "ID:";
            lblId.Location = new Point(450, 20);
            lblId.Size = new Size(80, 20);

            lblCantidad.Text = "Cantidad:";
            lblCantidad.Location = new Point(450, 50);
            lblCantidad.Size = new Size(80, 20);

            lblPrecio.Text = "Precio Unitario:";
            lblPrecio.Location = new Point(450, 80);
            lblPrecio.Size = new Size(100, 20);

            lblPedidoId.Text = "Pedido ID:";
            lblPedidoId.Location = new Point(450, 110);
            lblPedidoId.Size = new Size(100, 20);

            lblProductoId.Text = "Producto ID:";
            lblProductoId.Location = new Point(450, 140);
            lblProductoId.Size = new Size(100, 20);

            // TextBoxes
            txtId.Location = new Point(560, 20);
            txtId.Size = new Size(100, 25);

            txtCantidad.Location = new Point(560, 50);
            txtCantidad.Size = new Size(100, 25);

            txtPrecio.Location = new Point(560, 80);
            txtPrecio.Size = new Size(100, 25);

            txtPedidoId.Location = new Point(560, 110);
            txtPedidoId.Size = new Size(100, 25);

            txtProductoId.Location = new Point(560, 140);
            txtProductoId.Size = new Size(100, 25);

            // DataGridView
            dgvDetallePedido.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetallePedido.Location = new Point(12, 180);
            dgvDetallePedido.Name = "dgvDetallePedido";
            dgvDetallePedido.RowHeadersWidth = 51;
            dgvDetallePedido.Size = new Size(760, 250);
            dgvDetallePedido.TabIndex = 0;

            // Botones
            btnCargar.Location = new Point(12, 20);
            btnCargar.Name = "btnCargar";
            btnCargar.Size = new Size(94, 29);
            btnCargar.TabIndex = 1;
            btnCargar.Text = "Cargar";
            btnCargar.UseVisualStyleBackColor = true;
            btnCargar.Click += btnCargar_Click;

            btnCrear.Location = new Point(120, 20);
            btnCrear.Name = "btnCrear";
            btnCrear.Size = new Size(94, 29);
            btnCrear.TabIndex = 2;
            btnCrear.Text = "Crear";
            btnCrear.UseVisualStyleBackColor = true;
            btnCrear.Click += btnCrear_Click;

            btnActualizar.Location = new Point(230, 20);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(94, 29);
            btnActualizar.TabIndex = 3;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click;

            btnEliminar.Location = new Point(340, 20);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(94, 29);
            btnEliminar.TabIndex = 4;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;

            // Form
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 450);
            Controls.Add(dgvDetallePedido);
            Controls.Add(btnCargar);
            Controls.Add(btnCrear);
            Controls.Add(btnActualizar);
            Controls.Add(btnEliminar);

            Controls.Add(lblId);
            Controls.Add(lblCantidad);
            Controls.Add(lblPrecio);
            Controls.Add(lblPedidoId);
            Controls.Add(lblProductoId);

            Controls.Add(txtId);
            Controls.Add(txtCantidad);
            Controls.Add(txtPrecio);
            Controls.Add(txtPedidoId);
            Controls.Add(txtProductoId);

            Name = "Form1";
            Text = "Gestión de Detalles de Pedido";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDetallePedido).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}