namespace SellPoint.View
{
    partial class CuponForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvCupones;
        private System.Windows.Forms.Button btnCargar;
        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.TextBox txtDescuento;
        private System.Windows.Forms.TextBox txtFechaVencimiento;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.Label lblDescuento;
        private System.Windows.Forms.Label lblFechaVencimiento;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvCupones = new System.Windows.Forms.DataGridView();
            this.btnCargar = new System.Windows.Forms.Button();
            this.btnCrear = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.txtId = new System.Windows.Forms.TextBox();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.txtDescuento = new System.Windows.Forms.TextBox();
            this.txtFechaVencimiento = new System.Windows.Forms.TextBox();
            this.lblId = new System.Windows.Forms.Label();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.lblDescuento = new System.Windows.Forms.Label();
            this.lblFechaVencimiento = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvCupones)).BeginInit();
            this.SuspendLayout();

            // dgvCupones
            this.dgvCupones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCupones.Location = new System.Drawing.Point(20, 220);
            this.dgvCupones.Name = "dgvCupones";
            this.dgvCupones.Size = new System.Drawing.Size(600, 200);
            this.dgvCupones.TabIndex = 0;

            // txtId
            this.txtId.Location = new System.Drawing.Point(140, 20);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(150, 23);

            // txtCodigo
            this.txtCodigo.Location = new System.Drawing.Point(140, 60);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(150, 23);

            // txtDescuento
            this.txtDescuento.Location = new System.Drawing.Point(140, 100);
            this.txtDescuento.Name = "txtDescuento";
            this.txtDescuento.Size = new System.Drawing.Size(150, 23);

            // txtFechaVencimiento
            this.txtFechaVencimiento.Location = new System.Drawing.Point(140, 140);
            this.txtFechaVencimiento.Name = "txtFechaVencimiento";
            this.txtFechaVencimiento.Size = new System.Drawing.Size(150, 23);

            // lblId
            this.lblId.Text = "ID:";
            this.lblId.Location = new System.Drawing.Point(20, 20);
            this.lblId.Size = new System.Drawing.Size(100, 20);

            // lblCodigo
            this.lblCodigo.Text = "Código:";
            this.lblCodigo.Location = new System.Drawing.Point(20, 60);
            this.lblCodigo.Size = new System.Drawing.Size(100, 20);

            // lblDescuento
            this.lblDescuento.Text = "Descuento:";
            this.lblDescuento.Location = new System.Drawing.Point(20, 100);
            this.lblDescuento.Size = new System.Drawing.Size(100, 20);

            // lblFechaVencimiento
            this.lblFechaVencimiento.Text = "Vence:";
            this.lblFechaVencimiento.Location = new System.Drawing.Point(20, 140);
            this.lblFechaVencimiento.Size = new System.Drawing.Size(100, 20);

            // btnCargar
            this.btnCargar.Text = "Cargar";
            this.btnCargar.Location = new System.Drawing.Point(320, 20);
            this.btnCargar.Size = new System.Drawing.Size(100, 30);
            this.btnCargar.Click += new System.EventHandler(this.btnCargar_Click);

            // btnCrear
            this.btnCrear.Text = "Crear";
            this.btnCrear.Location = new System.Drawing.Point(320, 60);
            this.btnCrear.Size = new System.Drawing.Size(100, 30);
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);

            // btnActualizar
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.Location = new System.Drawing.Point(320, 100);
            this.btnActualizar.Size = new System.Drawing.Size(100, 30);
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);

            // btnEliminar
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Location = new System.Drawing.Point(320, 140);
            this.btnEliminar.Size = new System.Drawing.Size(100, 30);
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);

            // CuponForm
            this.ClientSize = new System.Drawing.Size(650, 450);
            this.Controls.Add(this.dgvCupones);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.txtDescuento);
            this.Controls.Add(this.txtFechaVencimiento);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.lblCodigo);
            this.Controls.Add(this.lblDescuento);
            this.Controls.Add(this.lblFechaVencimiento);
            this.Controls.Add(this.btnCargar);
            this.Controls.Add(this.btnCrear);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.btnEliminar);
            this.Name = "CuponForm";
            this.Text = "Gestión de Cupones";
            this.Load += new System.EventHandler(this.CuponForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCupones)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
