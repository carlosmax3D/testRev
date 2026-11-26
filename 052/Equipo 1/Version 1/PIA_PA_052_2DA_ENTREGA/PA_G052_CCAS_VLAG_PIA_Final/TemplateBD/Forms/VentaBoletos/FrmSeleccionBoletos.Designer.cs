namespace TemplateCSharp.Forms.VentaBoletos
{
    partial class FrmSeleccionBoletos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSeleccionBoletos));
            label1 = new Label();
            label2 = new Label();
            cmbZona = new ComboBox();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            label6 = new Label();
            lblVistaAsiento = new Label();
            btnConfirmar = new Button();
            dgvDetalleBoleto = new DataGridView();
            NombreZona = new DataGridViewTextBoxColumn();
            Fila = new DataGridViewTextBoxColumn();
            NumeroAsiento = new DataGridViewTextBoxColumn();
            PrecioUnitario = new DataGridViewTextBoxColumn();
            btnCancelar = new Button();
            lblSubtotal = new Label();
            lblIva = new Label();
            lblTotal = new Label();
            lblResumenPartido = new Label();
            picMapaEstadio = new PictureBox();
            label7 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvDetalleBoleto).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picMapaEstadio).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            label1.Location = new Point(16, 16);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(152, 41);
            label1.TabIndex = 0;
            label1.Text = "3. Boletos";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Calisto MT", 10.8F);
            label2.Location = new Point(34, 83);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(61, 21);
            label2.TabIndex = 1;
            label2.Text = "Zona: ";
            // 
            // cmbZona
            // 
            cmbZona.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbZona.FormattingEnabled = true;
            cmbZona.Items.AddRange(new object[] { "Preferentemente" });
            cmbZona.Location = new Point(111, 75);
            cmbZona.Margin = new Padding(4, 3, 4, 3);
            cmbZona.Name = "cmbZona";
            cmbZona.Size = new Size(389, 29);
            cmbZona.TabIndex = 2;
            cmbZona.SelectedIndexChanged += cmbZona_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Calisto MT", 10.8F);
            label3.Location = new Point(539, 78);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(68, 21);
            label3.TabIndex = 3;
            label3.Text = "Precio: ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Calisto MT", 10.8F);
            label4.Location = new Point(626, 78);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(51, 21);
            label4.TabIndex = 4;
            label4.Text = "$0.00";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Calisto MT", 10.8F);
            label5.Location = new Point(33, 132);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(172, 21);
            label5.TabIndex = 5;
            label5.Text = "Asientos disponibles:";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Location = new Point(34, 156);
            flowLayoutPanel1.Margin = new Padding(4, 3, 4, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(797, 236);
            flowLayoutPanel1.TabIndex = 6;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Calisto MT", 10.8F);
            label6.Location = new Point(40, 418);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(191, 21);
            label6.TabIndex = 7;
            label6.Text = "Asientos seleccionados:";
            // 
            // lblVistaAsiento
            // 
            lblVistaAsiento.AutoSize = true;
            lblVistaAsiento.Font = new Font("Calisto MT", 10.8F);
            lblVistaAsiento.Location = new Point(275, 418);
            lblVistaAsiento.Margin = new Padding(4, 0, 4, 0);
            lblVistaAsiento.Name = "lblVistaAsiento";
            lblVistaAsiento.Size = new Size(48, 21);
            lblVistaAsiento.TabIndex = 8;
            lblVistaAsiento.Text = "0 / 4";
            // 
            // btnConfirmar
            // 
            btnConfirmar.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnConfirmar.Location = new Point(644, 653);
            btnConfirmar.Margin = new Padding(4, 3, 4, 3);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(187, 48);
            btnConfirmar.TabIndex = 9;
            btnConfirmar.Text = "Confirmar";
            btnConfirmar.UseVisualStyleBackColor = true;
            btnConfirmar.Click += btnConfirmar_Click;
            // 
            // dgvDetalleBoleto
            // 
            dgvDetalleBoleto.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetalleBoleto.Columns.AddRange(new DataGridViewColumn[] { NombreZona, Fila, NumeroAsiento, PrecioUnitario });
            dgvDetalleBoleto.Location = new Point(34, 463);
            dgvDetalleBoleto.Margin = new Padding(4, 3, 4, 3);
            dgvDetalleBoleto.Name = "dgvDetalleBoleto";
            dgvDetalleBoleto.ReadOnly = true;
            dgvDetalleBoleto.RowHeadersWidth = 51;
            dgvDetalleBoleto.Size = new Size(632, 150);
            dgvDetalleBoleto.TabIndex = 10;
            // 
            // NombreZona
            // 
            NombreZona.HeaderText = "Zona";
            NombreZona.MinimumWidth = 6;
            NombreZona.Name = "NombreZona";
            NombreZona.ReadOnly = true;
            NombreZona.Width = 125;
            // 
            // Fila
            // 
            Fila.HeaderText = "Fila";
            Fila.MinimumWidth = 6;
            Fila.Name = "Fila";
            Fila.ReadOnly = true;
            Fila.Width = 125;
            // 
            // NumeroAsiento
            // 
            NumeroAsiento.HeaderText = "Asiento";
            NumeroAsiento.MinimumWidth = 6;
            NumeroAsiento.Name = "NumeroAsiento";
            NumeroAsiento.ReadOnly = true;
            NumeroAsiento.Width = 125;
            // 
            // PrecioUnitario
            // 
            PrecioUnitario.HeaderText = "Precio";
            PrecioUnitario.MinimumWidth = 6;
            PrecioUnitario.Name = "PrecioUnitario";
            PrecioUnitario.ReadOnly = true;
            PrecioUnitario.Width = 125;
            // 
            // btnCancelar
            // 
            btnCancelar.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCancelar.Location = new Point(313, 653);
            btnCancelar.Margin = new Padding(4, 3, 4, 3);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(187, 48);
            btnCancelar.TabIndex = 11;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // lblSubtotal
            // 
            lblSubtotal.AutoSize = true;
            lblSubtotal.Location = new Point(833, 463);
            lblSubtotal.Name = "lblSubtotal";
            lblSubtotal.Size = new Size(124, 21);
            lblSubtotal.TabIndex = 12;
            lblSubtotal.Text = "Subtotal: $0.00";
            // 
            // lblIva
            // 
            lblIva.AutoSize = true;
            lblIva.Location = new Point(812, 516);
            lblIva.Name = "lblIva";
            lblIva.Size = new Size(145, 21);
            lblIva.TabIndex = 13;
            lblIva.Text = "IVA (16%): $0.00";
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Calisto MT", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotal.Location = new Point(855, 567);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(108, 21);
            lblTotal.TabIndex = 14;
            lblTotal.Text = "Total: $0.00";
            // 
            // lblResumenPartido
            // 
            lblResumenPartido.AutoSize = true;
            lblResumenPartido.Font = new Font("Segoe UI Black", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblResumenPartido.Location = new Point(196, 29);
            lblResumenPartido.Name = "lblResumenPartido";
            lblResumenPartido.Size = new Size(0, 25);
            lblResumenPartido.TabIndex = 15;
            // 
            // picMapaEstadio
            // 
            picMapaEstadio.Location = new Point(855, 131);
            picMapaEstadio.Name = "picMapaEstadio";
            picMapaEstadio.Size = new Size(500, 308);
            picMapaEstadio.SizeMode = PictureBoxSizeMode.Zoom;
            picMapaEstadio.TabIndex = 16;
            picMapaEstadio.TabStop = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Calisto MT", 10.8F);
            label7.Location = new Point(855, 97);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(249, 21);
            label7.TabIndex = 17;
            label7.Text = "Estadio seleccionado (imagen):";
            // 
            // FrmSeleccionBoletos
            // 
            AutoScaleDimensions = new SizeF(11F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(1367, 724);
            Controls.Add(label7);
            Controls.Add(picMapaEstadio);
            Controls.Add(lblResumenPartido);
            Controls.Add(lblTotal);
            Controls.Add(lblIva);
            Controls.Add(lblSubtotal);
            Controls.Add(btnCancelar);
            Controls.Add(dgvDetalleBoleto);
            Controls.Add(btnConfirmar);
            Controls.Add(lblVistaAsiento);
            Controls.Add(label6);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(cmbZona);
            Controls.Add(label2);
            Controls.Add(label1);
            Font = new Font("Calisto MT", 10.8F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            Name = "FrmSeleccionBoletos";
            Text = "Venta de Boletos";
            Load += FrmSeleccionBoletos_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDetalleBoleto).EndInit();
            ((System.ComponentModel.ISupportInitialize)picMapaEstadio).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private ComboBox cmbZona;
        private Label label3;
        private Label label4;
        private Label label5;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label label6;
        private Label lblVistaAsiento;
        private Button btnConfirmar;
        private DataGridView dgvDetalleBoleto;
        private Button btnCancelar;
        private Label lblSubtotal;
        private Label lblIva;
        private Label lblTotal;
        private DataGridViewTextBoxColumn NombreZona;
        private DataGridViewTextBoxColumn Fila;
        private DataGridViewTextBoxColumn NumeroAsiento;
        private DataGridViewTextBoxColumn PrecioUnitario;
        private Label lblResumenPartido;
        private PictureBox picMapaEstadio;
        private Label label7;
    }
}