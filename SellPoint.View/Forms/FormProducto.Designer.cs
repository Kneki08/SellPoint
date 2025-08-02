namespace SellPoint.View
{
    partial class FormProducto
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        private void InitializeComponent()
        {
            this.txtId = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.txtPrecio = new System.Windows.Forms.TextBox();
            this.txtStock = new System.Windows.Forms.TextBox();

            this.lblId = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.lblStock = new System.Windows.Forms.Label();

            this.btnCrear = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnCargar = new System.Windows.Forms.Button();

            this.dgvProductos = new System.Windows.Forms.DataGridView();

            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            this.SuspendLayout();

            // txtId
            this.txtId.Location = new System.Drawing.Point(130, 20);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(180, 23);

            // txtNombre
            this.txtNombre.Location = new System.Drawing.Point(130, 60);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(180, 23);

            // txtDescripcion
            this.txtDescripcion.Location = new System.Drawing.Point(130, 100);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(180, 23);

            // txtPrecio
            this.txtPrecio.Location = new System.Drawing.Point(130, 140);
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Size = new System.Drawing.Size(180, 23);

            // txtStock
            this.txtStock.Location = new System.Drawing.Point(130, 180);
            this.txtStock.Name = "txtStock";
            this.txtStock.Size = new System.Drawing.Size(180, 23);

            // lblId
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(30, 23);
            this.lblId.Text = "ID Producto:";

            // lblNombre
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(30, 63);
            this.lblNombre.Text = "Nombre:";

            // lblDescripcion
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Location = new System.Drawing.Point(30, 103);
            this.lblDescripcion.Text = "Descripción:";

            // lblPrecio
            this.lblPrecio.AutoSize = true;
            this.lblPrecio.Location = new System.Drawing.Point(30, 143);
            this.lblPrecio.Text = "Precio:";

            // lblStock
            this.lblStock.AutoSize = true;
            this.lblStock.Location = new System.Drawing.Point(30, 183);
            this.lblStock.Text = "Stock:";

            // btnCrear
            this.btnCrear.Location = new System.Drawing.Point(350, 20);
            this.btnCrear.Size = new System.Drawing.Size(100, 30);
            this.btnCrear.Text = "Crear";
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);

            // btnActualizar
            this.btnActualizar.Location = new System.Drawing.Point(350, 60);
            this.btnActualizar.Size = new System.Drawing.Size(100, 30);
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);

            // btnEliminar
            this.btnEliminar.Location = new System.Drawing.Point(350, 100);
            this.btnEliminar.Size = new System.Drawing.Size(100, 30);
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);

            // btnCargar
            this.btnCargar.Location = new System.Drawing.Point(350, 140);
            this.btnCargar.Size = new System.Drawing.Size(100, 30);
            this.btnCargar.Text = "Cargar";
            this.btnCargar.Click += new System.EventHandler(this.btnCargar_Click);

            // dgvProductos
            this.dgvProductos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                                          | System.Windows.Forms.AnchorStyles.Right))));
            this.dgvProductos.Location = new System.Drawing.Point(30, 230);
            this.dgvProductos.Size = new System.Drawing.Size(540, 200);
            this.dgvProductos.RowTemplate.Height = 25;
            this.dgvProductos.Name = "dgvProductos";

            // FormProducto
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.ClientSize = new System.Drawing.Size(600, 460);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.txtPrecio);
            this.Controls.Add(this.txtStock);

            this.Controls.Add(this.lblId);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.lblPrecio);
            this.Controls.Add(this.lblStock);

            this.Controls.Add(this.btnCrear);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnCargar);

            this.Controls.Add(this.dgvProductos);
            this.Name = "FormProducto";
            this.Text = "Gestión de Productos";
            this.Load += new System.EventHandler(this.FormProducto_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.TextBox txtPrecio;
        private System.Windows.Forms.TextBox txtStock;

        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.Label lblStock;

        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnCargar;

        private System.Windows.Forms.DataGridView dgvProductos;
    }
}
