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

            ((System.ComponentModel.ISupportInitialize)dgvCupones).BeginInit();
            SuspendLayout();

            // === GroupBox Datos ===
            grpDatos.Text = "Datos del Cupón";
            grpDatos.Location = new Point(20, 20);
            grpDatos.Size = new Size(440, 180);
            grpDatos.Controls.AddRange(new Control[]
            {
                lblId, txtId, lblCodigo, txtCodigo,
                lblDescuento, txtDescuento,
                lblFechaVencimiento, dtpFechaVencimiento
            });

            // === Labels y TextBoxes ===
            lblId.Text = "ID:";
            lblId.Location = new Point(20, 25);
            txtId.Location = new Point(150, 22);
            txtId.Size = new Size(250, 23);

            lblCodigo.Text = "Código:";
            lblCodigo.Location = new Point(20, 55);
            txtCodigo.Location = new Point(150, 52);
            txtCodigo.Size = new Size(250, 23);

            lblDescuento.Text = "Descuento:";
            lblDescuento.Location = new Point(20, 85);
            txtDescuento.Location = new Point(150, 82);
            txtDescuento.Size = new Size(250, 23);

            lblFechaVencimiento.Text = "Fecha Vencimiento:";
            lblFechaVencimiento.Location = new Point(20, 115);
            dtpFechaVencimiento.Location = new Point(150, 112);
            dtpFechaVencimiento.Size = new Size(250, 23);
            dtpFechaVencimiento.Format = DateTimePickerFormat.Short;

            // === Panel de Botones ===
            pnlBotones.Location = new Point(480, 20);
            pnlBotones.Size = new Size(140, 180);
            pnlBotones.FlowDirection = FlowDirection.TopDown;
            pnlBotones.Controls.AddRange(new Control[] {
                btnCargar, btnCrear, btnActualizar, btnEliminar
            });

            // === Botones ===
            ConfigurarBoton(btnCargar, "Cargar", "Cargar cupones");
            ConfigurarBoton(btnCrear, "Crear", "Crear nuevo cupón");
            ConfigurarBoton(btnActualizar, "Actualizar", "Actualizar cupón");
            ConfigurarBoton(btnEliminar, "Eliminar", "Eliminar cupón");

            // === DataGridView ===
            dgvCupones.Location = new Point(20, 220);
            dgvCupones.Size = new Size(600, 200);
            dgvCupones.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            dgvCupones.ReadOnly = true;
            dgvCupones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCupones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // === Form ===
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(650, 450);
            Controls.AddRange(new Control[] {
                grpDatos, pnlBotones, dgvCupones
            });
            Font = new Font("Segoe UI", 10F);
            Name = "FormCupon";
            Text = "Gestión de Cupones";

            ((System.ComponentModel.ISupportInitialize)dgvCupones).EndInit();
            ResumeLayout(false);
        }

        private void ConfigurarBoton(Button btn, string texto, string tooltipText)
        {
            btn.Size = new Size(120, 30);
            btn.Text = texto;
            toolTip.SetToolTip(btn, tooltipText);
        }
    }
}


