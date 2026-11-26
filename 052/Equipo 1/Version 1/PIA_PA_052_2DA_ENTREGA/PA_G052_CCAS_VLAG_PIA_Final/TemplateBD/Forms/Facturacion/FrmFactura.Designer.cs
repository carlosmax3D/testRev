namespace TemplateCSharp.Forms.Facturacion
{
    partial class FrmFactura
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

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFactura));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            dataGridView1 = new DataGridView();
            label12 = new Label();
            label13 = new Label();
            label14 = new Label();
            label15 = new Label();
            btnExportar = new Button();
            label16 = new Label();
            lblMetodoPago = new Label();
            Partido = new DataGridViewTextBoxColumn();
            Estadio = new DataGridViewTextBoxColumn();
            Zona = new DataGridViewTextBoxColumn();
            Fila = new DataGridViewTextBoxColumn();
            Asiento = new DataGridViewTextBoxColumn();
            Precio = new DataGridViewTextBoxColumn();
            Cantidad = new DataGridViewTextBoxColumn();
            Subtotal = new DataGridViewTextBoxColumn();
            Iva = new DataGridViewTextBoxColumn();
            Total = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Calisto MT", 10.8F);
            label1.Location = new Point(33, 55);
            label1.Name = "label1";
            label1.Size = new Size(56, 21);
            label1.TabIndex = 0;
            label1.Text = "Folio:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Calisto MT", 10.8F);
            label2.Location = new Point(33, 80);
            label2.Name = "label2";
            label2.Size = new Size(61, 21);
            label2.TabIndex = 1;
            label2.Text = "Fecha:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Calisto MT", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(584, 80);
            label3.Name = "label3";
            label3.Size = new Size(54, 21);
            label3.TabIndex = 2;
            label3.Text = "Hora:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(33, 120);
            label4.Name = "label4";
            label4.Size = new Size(68, 25);
            label4.TabIndex = 3;
            label4.Text = "Emisor";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Calisto MT", 10.2F);
            label5.Location = new Point(37, 157);
            label5.Name = "label5";
            label5.Size = new Size(77, 20);
            label5.TabIndex = 4;
            label5.Text = "Empresa:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Calisto MT", 10.2F);
            label6.Location = new Point(37, 188);
            label6.Name = "label6";
            label6.Size = new Size(49, 20);
            label6.TabIndex = 5;
            label6.Text = "RFC:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Calisto MT", 10.2F);
            label7.Location = new Point(37, 218);
            label7.Name = "label7";
            label7.Size = new Size(88, 20);
            label7.TabIndex = 6;
            label7.Text = "Domicilio:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.Location = new Point(584, 120);
            label8.Name = "label8";
            label8.Size = new Size(92, 25);
            label8.TabIndex = 7;
            label8.Text = "Receptor:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Calisto MT", 10.8F);
            label9.Location = new Point(584, 157);
            label9.Name = "label9";
            label9.Size = new Size(78, 21);
            label9.TabIndex = 8;
            label9.Text = "Nombre:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Calisto MT", 10.8F);
            label10.Location = new Point(584, 188);
            label10.Name = "label10";
            label10.Size = new Size(52, 21);
            label10.TabIndex = 9;
            label10.Text = "RFC:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Calisto MT", 10.8F);
            label11.Location = new Point(584, 218);
            label11.Name = "label11";
            label11.Size = new Size(93, 21);
            label11.TabIndex = 10;
            label11.Text = "Domicilio:";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Partido, Estadio, Zona, Fila, Asiento, Precio, Cantidad, Subtotal, Iva, Total });
            dataGridView1.Location = new Point(12, 275);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1309, 186);
            dataGridView1.TabIndex = 11;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(484, 481);
            label12.Name = "label12";
            label12.Size = new Size(68, 20);
            label12.TabIndex = 12;
            label12.Text = "Subtotal:";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(484, 513);
            label13.Name = "label13";
            label13.Size = new Size(34, 20);
            label13.TabIndex = 13;
            label13.Text = "IVA:";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label14.Location = new Point(484, 545);
            label14.Name = "label14";
            label14.Size = new Size(72, 25);
            label14.TabIndex = 14;
            label14.Text = "TOTAL:";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label15.Location = new Point(3, 9);
            label15.Name = "label15";
            label15.Size = new Size(165, 37);
            label15.TabIndex = 15;
            label15.Text = "Facturacion";
            // 
            // btnExportar
            // 
            btnExportar.Location = new Point(621, 603);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(94, 29);
            btnExportar.TabIndex = 16;
            btnExportar.Text = "Exportar Factura";
            btnExportar.UseVisualStyleBackColor = true;
            btnExportar.Click += btnExportar_Click;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Calisto MT", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label16.Location = new Point(584, 55);
            label16.Name = "label16";
            label16.Size = new Size(68, 21);
            label16.TabIndex = 17;
            label16.Text = "Estado:";
            // 
            // lblMetodoPago
            // 
            lblMetodoPago.AutoSize = true;
            lblMetodoPago.Location = new Point(121, 481);
            lblMetodoPago.Name = "lblMetodoPago";
            lblMetodoPago.Size = new Size(125, 20);
            lblMetodoPago.TabIndex = 18;
            lblMetodoPago.Text = "Metodo De Pago:";
            // 
            // Partido
            // 
            Partido.HeaderText = "Partido";
            Partido.MinimumWidth = 6;
            Partido.Name = "Partido";
            Partido.ReadOnly = true;
            Partido.Width = 125;
            // 
            // Estadio
            // 
            Estadio.DataPropertyName = "NombreEstadio";
            Estadio.HeaderText = "Estadio";
            Estadio.MinimumWidth = 6;
            Estadio.Name = "Estadio";
            Estadio.ReadOnly = true;
            Estadio.Width = 125;
            // 
            // Zona
            // 
            Zona.HeaderText = "Zona";
            Zona.MinimumWidth = 6;
            Zona.Name = "Zona";
            Zona.ReadOnly = true;
            Zona.Width = 125;
            // 
            // Fila
            // 
            Fila.HeaderText = "Fila";
            Fila.MinimumWidth = 6;
            Fila.Name = "Fila";
            Fila.ReadOnly = true;
            Fila.Width = 125;
            // 
            // Asiento
            // 
            Asiento.HeaderText = "Asiento";
            Asiento.MinimumWidth = 6;
            Asiento.Name = "Asiento";
            Asiento.ReadOnly = true;
            Asiento.Width = 125;
            // 
            // Precio
            // 
            dataGridViewCellStyle1.Format = "C2";
            Precio.DefaultCellStyle = dataGridViewCellStyle1;
            Precio.HeaderText = "Precio";
            Precio.MinimumWidth = 6;
            Precio.Name = "Precio";
            Precio.ReadOnly = true;
            Precio.Width = 125;
            // 
            // Cantidad
            // 
            Cantidad.DataPropertyName = "Cantidad";
            Cantidad.HeaderText = "Cantidad";
            Cantidad.MinimumWidth = 6;
            Cantidad.Name = "Cantidad";
            Cantidad.ReadOnly = true;
            Cantidad.Width = 125;
            // 
            // Subtotal
            // 
            Subtotal.DataPropertyName = "Subtotal";
            dataGridViewCellStyle2.Format = "C2";
            Subtotal.DefaultCellStyle = dataGridViewCellStyle2;
            Subtotal.HeaderText = "Subtotal";
            Subtotal.MinimumWidth = 6;
            Subtotal.Name = "Subtotal";
            Subtotal.ReadOnly = true;
            Subtotal.Width = 125;
            // 
            // Iva
            // 
            Iva.DataPropertyName = "Iva";
            dataGridViewCellStyle3.Format = "C2";
            Iva.DefaultCellStyle = dataGridViewCellStyle3;
            Iva.HeaderText = "IVA";
            Iva.MinimumWidth = 6;
            Iva.Name = "Iva";
            Iva.ReadOnly = true;
            Iva.Width = 125;
            // 
            // Total
            // 
            Total.DataPropertyName = "Total";
            dataGridViewCellStyle4.Format = "C2";
            Total.DefaultCellStyle = dataGridViewCellStyle4;
            Total.HeaderText = "Total";
            Total.MinimumWidth = 6;
            Total.Name = "Total";
            Total.ReadOnly = true;
            Total.Width = 125;
            // 
            // FrmFactura
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1330, 665);
            Controls.Add(lblMetodoPago);
            Controls.Add(label16);
            Controls.Add(btnExportar);
            Controls.Add(label15);
            Controls.Add(label14);
            Controls.Add(label13);
            Controls.Add(label12);
            Controls.Add(dataGridView1);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FrmFactura";
            Load += FrmFactura_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private DataGridView dataGridView1;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Button btnExportar;
        private Label label16;
        private Label lblMetodoPago;
        private DataGridViewTextBoxColumn Partido;
        private DataGridViewTextBoxColumn Estadio;
        private DataGridViewTextBoxColumn Zona;
        private DataGridViewTextBoxColumn Fila;
        private DataGridViewTextBoxColumn Asiento;
        private DataGridViewTextBoxColumn Precio;
        private DataGridViewTextBoxColumn Cantidad;
        private DataGridViewTextBoxColumn Subtotal;
        private DataGridViewTextBoxColumn Iva;
        private DataGridViewTextBoxColumn Total;
    }
}
