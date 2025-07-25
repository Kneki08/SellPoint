using System;
using System.Drawing;
using System.Windows.Forms;

namespace SellPoint.View
{
    partial class CuponForm
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dgvCupones;
        private TextBox txtId;
        private TextBox txtCodigo;
        private TextBox txtValor;
        private DateTimePicker dtpFecha;
        private Button btnCargar;
        private Button btnCrear;
        private Button btnActualizar;
        private Button btnEliminar;
        private Label lblId;
        private Label lblCodigo;
        private Label lblValor;
        private Label lblFecha;
        private GroupBox grpDatos;
        private FlowLayoutPanel pnlBotones;
        private ToolTip toolTip;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            dgvCupones = new DataGridView();
            txtId = new TextBox();
            txtCodigo = new TextBox();
            txtValor = new TextBox();
            dtpFecha = new DateTimePicker();
            btnCargar = new Button();
            btnCrear = new Button();
            btnActualizar = new Button();
            btnEliminar = new Button();
            lblId = new Label();
            lblCodigo = new Label();
            lblValor = new Label();
            lblFecha = new Label();
            grpDatos = new GroupBox();
            pnlBotones = new FlowLayoutPanel();
            toolTip = new ToolTip(components);

            ((System.ComponentModel.ISupportInitialize)(dgvCupones)).BeginInit();
            SuspendLayout();

            // grpDatos
            grpDatos.Text = "Datos del Cupón";
            grpDatos.Location = new Point(20, 20);
            grpDatos.Size = new Size(440, 160);
            grpDatos.Controls.AddRange(new Control[] { lblId, txtId, lblCodigo, txtCodigo, lblValor, txtValor, lblFecha, dtpFecha });

            // lblId
            lblId.Location = new Point(20, 25);
            lblId.Size = new Size(60, 15);
            lblId.Text = "ID:";
            txtId.Location = new Point(100, 22);
            txtId.Size = new Size(300, 23);

            // lblCodigo
            lblCodigo.Location = new Point(20, 55);
            lblCodigo.Size = new Size(60, 15);
            lblCodigo.Text = "Código:";
            txtCodigo.Location = new Point(100, 52);
            txtCodigo.Size = new Size(300, 23);

            // lblValor
            lblValor.Location = new Point(20, 85);
            lblValor.Size = new Size(100, 15);
            lblValor.Text = "Valor Descuento:";
            txtValor.Location = new Point(130, 82);
            txtValor.Size = new Size(270, 23);

            // lblFecha
            lblFecha.Location = new Point(20, 115);
            lblFecha.Size = new Size(120, 15);
            lblFecha.Text = "Fecha Vencimiento:";
            dtpFecha.Location = new Point(150, 112);
            dtpFecha.Size = new Size(220, 23);
            dtpFecha.Format = DateTimePickerFormat.Short;

            // pnlBotones
            pnlBotones.Location = new Point(480, 20);
            pnlBotones.Size = new Size(140, 160);
            pnlBotones.FlowDirection = FlowDirection.TopDown;
            pnlBotones.Controls.AddRange(new Control[] { btnCargar, btnCrear, btnActualizar, btnEliminar });

            ConfigurarBoton(btnCargar, "Cargar", "Cargar todos los cupones");
            ConfigurarBoton(btnCrear, "Crear", "Crear nuevo cupón");
            ConfigurarBoton(btnActualizar, "Actualizar", "Actualizar el cupón actual");
            ConfigurarBoton(btnEliminar, "Eliminar", "Eliminar el cupón seleccionado");

            btnCargar.Click += btnCargar_Click;
            btnCrear.Click += btnCrear_Click;
            btnActualizar.Click += btnActualizar_Click;
            btnEliminar.Click += btnEliminar_Click;

            // dgvCupones
            dgvCupones.Location = new Point(20, 200);
            dgvCupones.Size = new Size(600, 200);

            // CuponForm
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(650, 420);
            Controls.AddRange(new Control[] { grpDatos, pnlBotones, dgvCupones });
            Name = "CuponForm";
            Text = "Gestión de Cupones";
            Load += CuponForm_Load;

            ((System.ComponentModel.ISupportInitialize)(dgvCupones)).EndInit();
            ResumeLayout(false);
        }

        // ✅ Fuera de InitializeComponent para evitar conflictos con el diseñador
        private void ConfigurarBoton(Button btn, string texto, string tooltip)
        {
            btn.Size = new Size(120, 25);
            btn.Text = texto;
            toolTip.SetToolTip(btn, tooltip);
        }
    }
}
