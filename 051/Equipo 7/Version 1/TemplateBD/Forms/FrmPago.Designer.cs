namespace TemplateCSharp.Forms
{
    partial class FrmPago
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
            textPago = new TextBox();
            label2 = new Label();
            textMetodoPago = new TextBox();
            label3 = new Label();
            textMonto = new TextBox();
            label4 = new Label();
            dateTimePicker1 = new DateTimePicker();
            label5 = new Label();
            textEstado = new TextBox();
            label6 = new Label();
            textVenta = new TextBox();
            dgvPago = new DataGridView();
            ColumnIdpago = new DataGridViewTextBoxColumn();
            ColumnMetodoPago = new DataGridViewLinkColumn();
            ColumnMonto = new DataGridViewTextBoxColumn();
            ColumnFechaPago = new DataGridViewTextBoxColumn();
            ColumnEstado = new DataGridViewTextBoxColumn();
            ColumnIdCompra = new DataGridViewTextBoxColumn();
            label7 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvPago).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 22);
            label1.Name = "label1";
            label1.Size = new Size(45, 20);
            label1.TabIndex = 0;
            label1.Text = "Pago:";
            // 
            // textPago
            // 
            textPago.Location = new Point(12, 54);
            textPago.Name = "textPago";
            textPago.Size = new Size(220, 27);
            textPago.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 102);
            label2.Name = "label2";
            label2.Size = new Size(102, 20);
            label2.TabIndex = 2;
            label2.Text = "Método Pago:";
            // 
            // textMetodoPago
            // 
            textMetodoPago.Location = new Point(12, 125);
            textMetodoPago.Name = "textMetodoPago";
            textMetodoPago.Size = new Size(220, 27);
            textMetodoPago.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 172);
            label3.Name = "label3";
            label3.Size = new Size(56, 20);
            label3.TabIndex = 4;
            label3.Text = "Monto:";
            // 
            // textMonto
            // 
            textMonto.Location = new Point(12, 195);
            textMonto.Name = "textMonto";
            textMonto.Size = new Size(220, 27);
            textMonto.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(18, 237);
            label4.Name = "label4";
            label4.Size = new Size(89, 20);
            label4.TabIndex = 6;
            label4.Text = "Fecha pago:";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(12, 260);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(250, 27);
            dateTimePicker1.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 309);
            label5.Name = "label5";
            label5.Size = new Size(57, 20);
            label5.TabIndex = 8;
            label5.Text = "Estado:";
            // 
            // textEstado
            // 
            textEstado.Location = new Point(12, 332);
            textEstado.Name = "textEstado";
            textEstado.Size = new Size(220, 27);
            textEstado.TabIndex = 9;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 380);
            label6.Name = "label6";
            label6.Size = new Size(49, 20);
            label6.TabIndex = 10;
            label6.Text = "Venta:";
            // 
            // textVenta
            // 
            textVenta.Location = new Point(12, 403);
            textVenta.Name = "textVenta";
            textVenta.Size = new Size(220, 27);
            textVenta.TabIndex = 11;
            // 
            // dgvPago
            // 
            dgvPago.AllowUserToAddRows = false;
            dgvPago.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvPago.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPago.Columns.AddRange(new DataGridViewColumn[] { ColumnIdpago, ColumnMetodoPago, ColumnMonto, ColumnFechaPago, ColumnEstado, ColumnIdCompra });
            dgvPago.Location = new Point(308, 75);
            dgvPago.Name = "dgvPago";
            dgvPago.ReadOnly = true;
            dgvPago.RowHeadersWidth = 51;
            dgvPago.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPago.Size = new Size(466, 269);
            dgvPago.TabIndex = 12;
            // 
            // ColumnIdpago
            // 
            ColumnIdpago.HeaderText = "IdPago";
            ColumnIdpago.MinimumWidth = 6;
            ColumnIdpago.Name = "ColumnIdpago";
            ColumnIdpago.ReadOnly = true;
            ColumnIdpago.Width = 84;
            // 
            // ColumnMetodoPago
            // 
            ColumnMetodoPago.HeaderText = "Método Pago";
            ColumnMetodoPago.MinimumWidth = 6;
            ColumnMetodoPago.Name = "ColumnMetodoPago";
            ColumnMetodoPago.ReadOnly = true;
            ColumnMetodoPago.Width = 95;
            // 
            // ColumnMonto
            // 
            ColumnMonto.HeaderText = "Monto";
            ColumnMonto.MinimumWidth = 6;
            ColumnMonto.Name = "ColumnMonto";
            ColumnMonto.ReadOnly = true;
            ColumnMonto.Width = 82;
            // 
            // ColumnFechaPago
            // 
            ColumnFechaPago.HeaderText = "Fecha Pago";
            ColumnFechaPago.MinimumWidth = 6;
            ColumnFechaPago.Name = "ColumnFechaPago";
            ColumnFechaPago.ReadOnly = true;
            ColumnFechaPago.Width = 104;
            // 
            // ColumnEstado
            // 
            ColumnEstado.HeaderText = "Estado";
            ColumnEstado.MinimumWidth = 6;
            ColumnEstado.Name = "ColumnEstado";
            ColumnEstado.ReadOnly = true;
            ColumnEstado.Width = 83;
            // 
            // ColumnIdCompra
            // 
            ColumnIdCompra.HeaderText = "IdCompra";
            ColumnIdCompra.MinimumWidth = 6;
            ColumnIdCompra.Name = "ColumnIdCompra";
            ColumnIdCompra.ReadOnly = true;
            ColumnIdCompra.Width = 104;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(308, 39);
            label7.Name = "label7";
            label7.Size = new Size(48, 20);
            label7.TabIndex = 13;
            label7.Text = "Pagos";
            // 
            // FrmPago
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label7);
            Controls.Add(dgvPago);
            Controls.Add(textVenta);
            Controls.Add(label6);
            Controls.Add(textEstado);
            Controls.Add(label5);
            Controls.Add(dateTimePicker1);
            Controls.Add(label4);
            Controls.Add(textMonto);
            Controls.Add(label3);
            Controls.Add(textMetodoPago);
            Controls.Add(label2);
            Controls.Add(textPago);
            Controls.Add(label1);
            Name = "FrmPago";
            Text = "Pago";
            ((System.ComponentModel.ISupportInitialize)dgvPago).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textPago;
        private Label label2;
        private TextBox textMetodoPago;
        private Label label3;
        private TextBox textMonto;
        private Label label4;
        private DateTimePicker dateTimePicker1;
        private Label label5;
        private TextBox textEstado;
        private Label label6;
        private TextBox textVenta;
        private DataGridView dgvPago;
        private DataGridViewTextBoxColumn ColumnIdpago;
        private DataGridViewLinkColumn ColumnMetodoPago;
        private DataGridViewTextBoxColumn ColumnMonto;
        private DataGridViewTextBoxColumn ColumnFechaPago;
        private DataGridViewTextBoxColumn ColumnEstado;
        private DataGridViewTextBoxColumn ColumnIdCompra;
        private Label label7;
    }
}