using System.Drawing;
using System.Windows.Forms;

namespace SellPoint.View.Forms
{
    partial class PedidoForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            dgvPedidos = new DataGridView();
            txtIdUsuario = new TextBox();
            txtNotas = new TextBox();
            txtTotal = new TextBox();
            txtCostoEnvio = new TextBox();
            txtDireccion = new TextBox();
            txtDescuento = new TextBox();
            txtReferencia = new TextBox();
            txtMetodoPago = new TextBox();
            txtSubtotal = new TextBox();
            label1 = new Label();
            label9 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            btnAgregar = new Button();
            btnActualizar = new Button();
            btnEliminar = new Button();
            btnCargar = new Button();
            btnLimpiar = new Button();
            cmbEstado = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dgvPedidos).BeginInit();
            SuspendLayout();
            // 
            // dgvPedidos
            // 
            dgvPedidos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPedidos.Location = new Point(2, 332);
            dgvPedidos.Name = "dgvPedidos";
            dgvPedidos.RowHeadersWidth = 51;
            dgvPedidos.Size = new Size(908, 421);
            dgvPedidos.TabIndex = 0;
            // 
            // txtIdUsuario
            // 
            txtIdUsuario.Location = new Point(935, 347);
            txtIdUsuario.Name = "txtIdUsuario";
            txtIdUsuario.Size = new Size(125, 27);
            txtIdUsuario.TabIndex = 1;
            // 
            // txtNotas
            // 
            txtNotas.Location = new Point(935, 687);
            txtNotas.Name = "txtNotas";
            txtNotas.Size = new Size(125, 27);
            txtNotas.TabIndex = 2;
            // 
            // txtTotal
            // 
            txtTotal.Location = new Point(935, 645);
            txtTotal.Name = "txtTotal";
            txtTotal.Size = new Size(125, 27);
            txtTotal.TabIndex = 3;
            // 
            // txtCostoEnvio
            // 
            txtCostoEnvio.Location = new Point(935, 599);
            txtCostoEnvio.Name = "txtCostoEnvio";
            txtCostoEnvio.Size = new Size(125, 27);
            txtCostoEnvio.TabIndex = 4;
            // 
            // txtDireccion
            // 
            txtDireccion.Location = new Point(935, 391);
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(125, 27);
            txtDireccion.TabIndex = 5;
            // 
            // txtDescuento
            // 
            txtDescuento.Location = new Point(935, 566);
            txtDescuento.Name = "txtDescuento";
            txtDescuento.Size = new Size(125, 27);
            txtDescuento.TabIndex = 6;
            // 
            // txtReferencia
            // 
            txtReferencia.Location = new Point(935, 478);
            txtReferencia.Name = "txtReferencia";
            txtReferencia.Size = new Size(125, 27);
            txtReferencia.TabIndex = 7;
            // 
            // txtMetodoPago
            // 
            txtMetodoPago.Location = new Point(935, 435);
            txtMetodoPago.Name = "txtMetodoPago";
            txtMetodoPago.Size = new Size(125, 27);
            txtMetodoPago.TabIndex = 8;
            // 
            // txtSubtotal
            // 
            txtSubtotal.Location = new Point(935, 523);
            txtSubtotal.Name = "txtSubtotal";
            txtSubtotal.Size = new Size(125, 27);
            txtSubtotal.TabIndex = 9;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.ActiveCaption;
            label1.Location = new Point(1085, 354);
            label1.Name = "label1";
            label1.Size = new Size(166, 20);
            label1.TabIndex = 10;
            label1.Text = " Ingresar ID del usuario ";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = SystemColors.ActiveCaption;
            label9.Location = new Point(1085, 398);
            label9.Name = "label9";
            label9.Size = new Size(179, 20);
            label9.TabIndex = 18;
            label9.Text = " ID de dirección de envío ";
            label9.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.ActiveCaption;
            label2.Location = new Point(1066, 690);
            label2.Name = "label2";
            label2.Size = new Size(56, 20);
            label2.TabIndex = 19;
            label2.Text = " Notas ";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.ActiveCaption;
            label3.Location = new Point(1085, 652);
            label3.Name = "label3";
            label3.Size = new Size(50, 20);
            label3.TabIndex = 20;
            label3.Text = " Total ";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = SystemColors.ActiveCaption;
            label4.Location = new Point(1085, 606);
            label4.Name = "label4";
            label4.Size = new Size(116, 20);
            label4.TabIndex = 21;
            label4.Text = " Costo de Envio ";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = SystemColors.ActiveCaption;
            label5.Location = new Point(1066, 569);
            label5.Name = "label5";
            label5.Size = new Size(87, 20);
            label5.TabIndex = 22;
            label5.Text = " Descuento ";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = SystemColors.ActiveCaption;
            label6.Location = new Point(1066, 521);
            label6.Name = "label6";
            label6.Size = new Size(73, 20);
            label6.TabIndex = 23;
            label6.Text = " Subtotal ";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = SystemColors.ActiveCaption;
            label7.Location = new Point(1066, 478);
            label7.Name = "label7";
            label7.Size = new Size(147, 20);
            label7.TabIndex = 24;
            label7.Text = " Referencia de pago ";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = SystemColors.ActiveCaption;
            label8.Location = new Point(1085, 438);
            label8.Name = "label8";
            label8.Size = new Size(128, 20);
            label8.TabIndex = 25;
            label8.Text = " Metodo de Pago ";
            label8.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(2, 297);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(136, 29);
            btnAgregar.TabIndex = 26;
            btnAgregar.Text = " Agregar Pedido";
            btnAgregar.UseVisualStyleBackColor = true;
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(144, 297);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(160, 29);
            btnActualizar.TabIndex = 27;
            btnActualizar.Text = " Actualizar Pedido";
            btnActualizar.UseVisualStyleBackColor = true;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(310, 297);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(125, 29);
            btnEliminar.TabIndex = 28;
            btnEliminar.Text = "Eliminar Pedido";
            btnEliminar.UseVisualStyleBackColor = true;
            // 
            // btnCargar
            // 
            btnCargar.Location = new Point(441, 297);
            btnCargar.Name = "btnCargar";
            btnCargar.Size = new Size(141, 29);
            btnCargar.TabIndex = 29;
            btnCargar.Text = " Cargar Pedido";
            btnCargar.UseVisualStyleBackColor = true;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(588, 297);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(87, 29);
            btnLimpiar.TabIndex = 30;
            btnLimpiar.Text = " Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // cmbEstado
            // 
            cmbEstado.FormattingEnabled = true;
            cmbEstado.Location = new Point(926, 297);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(151, 28);
            cmbEstado.TabIndex = 31;
            cmbEstado.Text = "Estado del pedido";
            // 
            // PedidoForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1475, 750);
            Controls.Add(cmbEstado);
            Controls.Add(btnLimpiar);
            Controls.Add(btnCargar);
            Controls.Add(btnEliminar);
            Controls.Add(btnActualizar);
            Controls.Add(btnAgregar);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label9);
            Controls.Add(label1);
            Controls.Add(txtSubtotal);
            Controls.Add(txtMetodoPago);
            Controls.Add(txtReferencia);
            Controls.Add(txtDescuento);
            Controls.Add(txtDireccion);
            Controls.Add(txtCostoEnvio);
            Controls.Add(txtTotal);
            Controls.Add(txtNotas);
            Controls.Add(txtIdUsuario);
            Controls.Add(dgvPedidos);
            Name = "PedidoForm";
            Text = "Gestión de Pedidos";
            ((System.ComponentModel.ISupportInitialize)dgvPedidos).EndInit();

            // === ENLACE DE EVENTOS ===
            btnAgregar.Click += btnAgregar_Click;
            btnActualizar.Click += btnActualizar_Click;
            btnEliminar.Click += btnEliminar_Click;
            btnCargar.Click += btnCargar_Click;
            btnLimpiar.Click += btnLimpiar_Click;
            dgvPedidos.SelectionChanged += dgvPedidos_SelectionChanged;

            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvPedidos;
        private TextBox txtIdUsuario;
        private TextBox txtNotas;
        private TextBox txtTotal;
        private TextBox txtCostoEnvio;
        private TextBox txtDireccion;
        private TextBox txtDescuento;
        private TextBox txtReferencia;
        private TextBox txtMetodoPago;
        private TextBox txtSubtotal;
        private Label label1;
        private Label label9;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Button btnAgregar;
        private Button btnActualizar;
        private Button btnEliminar;
        private Button btnCargar;
        private Button btnLimpiar;
        private ComboBox cmbEstado;
    }
}