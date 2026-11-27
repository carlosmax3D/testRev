namespace TemplateCSharp.Forms
{
    partial class FrmCompra
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            txtBuscarCliente = new TextBox();
            label2 = new Label();
            label3 = new Label();
            txtBuscarPartido = new TextBox();
            label4 = new Label();
            lbl = new Label();
            label6 = new Label();
            dgvPartidos = new DataGridView();
            btnBuscarCliente = new Button();
            cmbCliente = new ComboBox();
            btnNuevoCliente = new Button();
            btnBuscarPartido = new Button();
            cmbZona = new ComboBox();
            nudCantidad = new NumericUpDown();
            cmbMetodoPago = new ComboBox();
            label5 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            txtTitular = new TextBox();
            txtNumeroTarjeta = new TextBox();
            txtExpiracion = new TextBox();
            txtCCV = new TextBox();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            button1 = new Button();
            lblTotal = new Label();
            lblIVA = new Label();
            lblSubtotal = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvPartidos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(105, 20);
            label1.TabIndex = 0;
            label1.Text = "Buscar Cliente:";
            // 
            // txtBuscarCliente
            // 
            txtBuscarCliente.Location = new Point(12, 48);
            txtBuscarCliente.Name = "txtBuscarCliente";
            txtBuscarCliente.Size = new Size(253, 27);
            txtBuscarCliente.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(323, 9);
            label2.Name = "label2";
            label2.Size = new Size(55, 20);
            label2.TabIndex = 2;
            label2.Text = "Cliente";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 163);
            label3.Name = "label3";
            label3.Size = new Size(59, 20);
            label3.TabIndex = 4;
            label3.Text = "Partido:";
            // 
            // txtBuscarPartido
            // 
            txtBuscarPartido.Location = new Point(12, 186);
            txtBuscarPartido.Name = "txtBuscarPartido";
            txtBuscarPartido.Size = new Size(355, 27);
            txtBuscarPartido.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(497, 164);
            label4.Name = "label4";
            label4.Size = new Size(46, 20);
            label4.TabIndex = 6;
            label4.Text = "Zona:";
            // 
            // lbl
            // 
            lbl.AutoSize = true;
            lbl.Location = new Point(899, 254);
            lbl.Name = "lbl";
            lbl.Size = new Size(123, 20);
            lbl.TabIndex = 8;
            lbl.Text = "Metodo de Pago:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(723, 163);
            label6.Name = "label6";
            label6.Size = new Size(69, 20);
            label6.TabIndex = 10;
            label6.Text = "Personas:";
            // 
            // dgvPartidos
            // 
            dgvPartidos.AllowUserToAddRows = false;
            dgvPartidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPartidos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPartidos.Location = new Point(12, 221);
            dgvPartidos.Name = "dgvPartidos";
            dgvPartidos.ReadOnly = true;
            dgvPartidos.RowHeadersWidth = 51;
            dgvPartidos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPartidos.Size = new Size(861, 421);
            dgvPartidos.TabIndex = 12;
            dgvPartidos.SelectionChanged += dgvPartidos_SelectionChanged;
            // 
            // btnBuscarCliente
            // 
            btnBuscarCliente.Location = new Point(123, 9);
            btnBuscarCliente.Name = "btnBuscarCliente";
            btnBuscarCliente.Size = new Size(94, 29);
            btnBuscarCliente.TabIndex = 13;
            btnBuscarCliente.Text = "Buscar";
            btnBuscarCliente.UseVisualStyleBackColor = true;
            btnBuscarCliente.Click += btnBuscarCliente_Click;
            // 
            // cmbCliente
            // 
            cmbCliente.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCliente.FormattingEnabled = true;
            cmbCliente.Location = new Point(323, 48);
            cmbCliente.Name = "cmbCliente";
            cmbCliente.Size = new Size(220, 28);
            cmbCliente.TabIndex = 14;
            // 
            // btnNuevoCliente
            // 
            btnNuevoCliente.Location = new Point(12, 90);
            btnNuevoCliente.Name = "btnNuevoCliente";
            btnNuevoCliente.Size = new Size(94, 52);
            btnNuevoCliente.TabIndex = 15;
            btnNuevoCliente.Text = "Registrar Cliente";
            btnNuevoCliente.UseVisualStyleBackColor = true;
            btnNuevoCliente.Click += btnNuevoCliente_Click;
            // 
            // btnBuscarPartido
            // 
            btnBuscarPartido.Location = new Point(387, 186);
            btnBuscarPartido.Name = "btnBuscarPartido";
            btnBuscarPartido.Size = new Size(94, 29);
            btnBuscarPartido.TabIndex = 16;
            btnBuscarPartido.Text = "Buscar";
            btnBuscarPartido.UseVisualStyleBackColor = true;
            btnBuscarPartido.Click += btnBuscarPartido_Click;
            // 
            // cmbZona
            // 
            cmbZona.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbZona.FormattingEnabled = true;
            cmbZona.Location = new Point(497, 187);
            cmbZona.Name = "cmbZona";
            cmbZona.Size = new Size(220, 28);
            cmbZona.TabIndex = 17;
            cmbZona.SelectedIndexChanged += cmbZona_SelectedIndexChanged;
            // 
            // nudCantidad
            // 
            nudCantidad.Location = new Point(723, 186);
            nudCantidad.Maximum = new decimal(new int[] { 4, 0, 0, 0 });
            nudCantidad.Name = "nudCantidad";
            nudCantidad.Size = new Size(150, 27);
            nudCantidad.TabIndex = 18;
            nudCantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudCantidad.ValueChanged += nudCantidad_ValueChanged;
            // 
            // cmbMetodoPago
            // 
            cmbMetodoPago.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMetodoPago.FormattingEnabled = true;
            cmbMetodoPago.Location = new Point(899, 280);
            cmbMetodoPago.Name = "cmbMetodoPago";
            cmbMetodoPago.Size = new Size(220, 28);
            cmbMetodoPago.TabIndex = 19;
            cmbMetodoPago.SelectedIndexChanged += cmbMetodoPago_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(899, 327);
            label5.Name = "label5";
            label5.Size = new Size(54, 20);
            label5.TabIndex = 20;
            label5.Text = "Titular:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(899, 391);
            label7.Name = "label7";
            label7.Size = new Size(134, 20);
            label7.TabIndex = 21;
            label7.Text = "Numero de tarjeta:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(899, 459);
            label8.Name = "label8";
            label8.Size = new Size(81, 20);
            label8.TabIndex = 22;
            label8.Text = "Expiracion:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(1094, 459);
            label9.Name = "label9";
            label9.Size = new Size(39, 20);
            label9.TabIndex = 23;
            label9.Text = "CVV:";
            // 
            // txtTitular
            // 
            txtTitular.Location = new Point(899, 350);
            txtTitular.Name = "txtTitular";
            txtTitular.Size = new Size(279, 27);
            txtTitular.TabIndex = 24;
            // 
            // txtNumeroTarjeta
            // 
            txtNumeroTarjeta.Location = new Point(899, 414);
            txtNumeroTarjeta.MaxLength = 16;
            txtNumeroTarjeta.Name = "txtNumeroTarjeta";
            txtNumeroTarjeta.Size = new Size(279, 27);
            txtNumeroTarjeta.TabIndex = 25;
            // 
            // txtExpiracion
            // 
            txtExpiracion.Location = new Point(899, 482);
            txtExpiracion.MaxLength = 5;
            txtExpiracion.Name = "txtExpiracion";
            txtExpiracion.Size = new Size(149, 27);
            txtExpiracion.TabIndex = 26;
            // 
            // txtCCV
            // 
            txtCCV.Location = new Point(1090, 482);
            txtCCV.MaxLength = 4;
            txtCCV.Name = "txtCCV";
            txtCCV.Size = new Size(88, 27);
            txtCCV.TabIndex = 27;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(885, 543);
            label10.Name = "label10";
            label10.Size = new Size(68, 20);
            label10.TabIndex = 28;
            label10.Text = "Subtotal:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(919, 576);
            label11.Name = "label11";
            label11.Size = new Size(34, 20);
            label11.TabIndex = 29;
            label11.Text = "IVA:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(908, 622);
            label12.Name = "label12";
            label12.Size = new Size(45, 20);
            label12.TabIndex = 30;
            label12.Text = "Total:";
            // 
            // button1
            // 
            button1.Location = new Point(1113, 576);
            button1.Name = "button1";
            button1.Size = new Size(116, 60);
            button1.TabIndex = 31;
            button1.Text = "Confirmar Compra";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnConfirmarCompra_Click;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Unispace", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotal.Location = new Point(954, 624);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(0, 18);
            lblTotal.TabIndex = 34;
            // 
            // lblIVA
            // 
            lblIVA.AutoSize = true;
            lblIVA.Font = new Font("Unispace", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblIVA.Location = new Point(954, 578);
            lblIVA.Name = "lblIVA";
            lblIVA.Size = new Size(0, 18);
            lblIVA.TabIndex = 35;
            // 
            // lblSubtotal
            // 
            lblSubtotal.AutoSize = true;
            lblSubtotal.Font = new Font("Unispace", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSubtotal.Location = new Point(954, 545);
            lblSubtotal.Name = "lblSubtotal";
            lblSubtotal.Size = new Size(0, 18);
            lblSubtotal.TabIndex = 36;
            // 
            // FrmCompra
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1343, 654);
            Controls.Add(lblSubtotal);
            Controls.Add(lblIVA);
            Controls.Add(lblTotal);
            Controls.Add(button1);
            Controls.Add(label12);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(txtCCV);
            Controls.Add(txtExpiracion);
            Controls.Add(txtNumeroTarjeta);
            Controls.Add(txtTitular);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label5);
            Controls.Add(cmbMetodoPago);
            Controls.Add(nudCantidad);
            Controls.Add(cmbZona);
            Controls.Add(btnBuscarPartido);
            Controls.Add(btnNuevoCliente);
            Controls.Add(cmbCliente);
            Controls.Add(btnBuscarCliente);
            Controls.Add(dgvPartidos);
            Controls.Add(label6);
            Controls.Add(lbl);
            Controls.Add(label4);
            Controls.Add(txtBuscarPartido);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txtBuscarCliente);
            Controls.Add(label1);
            Name = "FrmCompra";
            Text = "Compra";
            Load += FrmCompra_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPartidos).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtBuscarCliente;
        private Label label2;
        private Label label3;
        private TextBox txtBuscarPartido;
        private Label label4;
        private Label lbl;
        private Label label6;
        private DataGridView dgvPartidos;
        private Button btnBuscarCliente;
        private ComboBox cmbCliente;
        private Button btnNuevoCliente;
        private Button btnBuscarPartido;
        private ComboBox cmbZona;
        private NumericUpDown nudCantidad;
        private ComboBox cmbMetodoPago;
        private Label label5;
        private Label label7;
        private Label label8;
        private Label label9;
        private TextBox txtTitular;
        private TextBox txtNumeroTarjeta;
        private TextBox txtExpiracion;
        private TextBox txtCCV;
        private Label label10;
        private Label label11;
        private Label label12;
        private Button button1;
        private Label lblTotal;
        private Label lblIVA;
        private Label lblSubtotal;
    }
}