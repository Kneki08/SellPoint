using System;
using System.Drawing;
using System.Windows.Forms;

namespace SellPoint.View
{
    partial class FormCupon
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dgvCupones;
        private TextBox txtId;
        private TextBox txtCodigo;
        private TextBox txtDescuento;
        private DateTimePicker dtpFechaVencimiento;
        private Button btnCargar;
        private Button btnCrear;
        private Button btnActualizar;
        private Button btnEliminar;
        private Label lblId;
        private Label lblCodigo;
        private Label lblDescuento;
        private Label lblFechaVencimiento;
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
            txtDescuento = new TextBox();
            dtpFechaVencimiento = new DateTimePicker();
            btnCargar = new Button();
            btnCrear = new Button();
            btnActualizar = new Button();
            btnEliminar = new Button();
            lblId = new Label();
            lblCodigo = new Label();
            lblDescuento = new Label();
            lblFechaVencimiento = new Label();
            grpDatos = new GroupBox();
            pnlBotones = new FlowLayoutPanel();
            toolTip = new ToolTip(components);

            ((System.ComponentModel.ISupportInitialize)(dgvCupones)).BeginInit();
            SuspendLayout();

            // grpDatos
            grpDatos.Text = "Datos del Cupón";
            grpDatos.Location = new Point(20, 20);
            grpDatos.Size = new Size(440, 160);
            grpDatos.Controls.AddRange(new Control[] {
                lblId, txtId, lblCodigo, txtCodigo,
                lblDescuento, txtDescuento,
                lblFechaVencimiento, dtpFechaVencimiento
            });

            // lblId
            lblId.Location = new Point(20, 25);
            lblId.Size = new Size(60, 15);
            lblId.Text = "ID:";
            txtId.Location = new Point(120, 22);
            txtId.Size = new Size(280, 23);

            // lblCodigo
            lblCodigo.Location = new Point(20, 55);
            lblCodigo.Size = new Size(60, 15);
            lblCodigo.Text = "Código:";
            txtCodigo.Location = new Point(120, 52);
            txtCodigo.Size = new Size(280, 23);

            // lblDescuento
            lblDescuento.Location = new Point(20, 85);
            lblDescuento.Size = new Size(90, 15);
            lblDescuento.Text = "Descuento:";
            txtDescuento.Location = new Point(120, 82);
            txtDescuento.Size = new Size(280, 23);

            // lblFechaVencimiento
            lblFechaVencimiento.Location = new Point(20, 115);
            lblFechaVencimiento.Size = new Size(120, 15);
            lblFechaVencimiento.Text = "Fecha Vencimiento:";
            dtpFechaVencimiento.Location = new Point(150, 112);
            dtpFechaVencimiento.Size = new Size(250, 23);
            dtpFechaVencimiento.Format = DateTimePickerFormat.Short;

            // pnlBotones
            pnlBotones.Location = new Point(480, 20);
            pnlBotones.Size = new Size(140, 160);
            pnlBotones.FlowDirection = FlowDirection.TopDown;
            pnlBotones.Controls.AddRange(new Control[] {
                btnCargar, btnCrear, btnActualizar, btnEliminar
            });

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

            // FormCupon
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(650, 430);
            Controls.AddRange(new Control[] {
                grpDatos, pnlBotones, dgvCupones
            });
            Name = "FormCupon";
            Text = "Gestión de Cupones";
            Load += FormCupon_Load;

            ((System.ComponentModel.ISupportInitialize)(dgvCupones)).EndInit();
            ResumeLayout(false);
        }

        private void ConfigurarBoton(Button btn, string texto, string tooltip)
        {
            btn.Size = new Size(120, 25);
            btn.Text = texto;
            toolTip.SetToolTip(btn, tooltip);
        }
    }
}

