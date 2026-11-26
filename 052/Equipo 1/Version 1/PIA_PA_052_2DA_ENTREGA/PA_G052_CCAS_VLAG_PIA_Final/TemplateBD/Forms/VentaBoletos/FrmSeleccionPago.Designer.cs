namespace TemplateCSharp.Forms.VentaBoletos
{
    partial class FrmSeleccionPago
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSeleccionPago));
            label1 = new Label();
            label2 = new Label();
            cmbMetodoPago = new ComboBox();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            lblSubtotal = new Label();
            btnCancelar = new Button();
            btnRealizarVenta = new Button();
            lblIVA = new Label();
            lblTotal = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            label1.Location = new Point(21, 21);
            label1.Name = "label1";
            label1.Size = new Size(119, 41);
            label1.TabIndex = 0;
            label1.Text = "4. Pago";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Calisto MT", 10.8F);
            label2.Location = new Point(21, 90);
            label2.Name = "label2";
            label2.Size = new Size(142, 21);
            label2.TabIndex = 1;
            label2.Text = "Metodo de Pago:";
            // 
            // cmbMetodoPago
            // 
            cmbMetodoPago.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMetodoPago.FormattingEnabled = true;
            cmbMetodoPago.Items.AddRange(new object[] { "Efectivo", "Tarjeta de crédito", "Tarjeta de débito" });
            cmbMetodoPago.Location = new Point(183, 90);
            cmbMetodoPago.Name = "cmbMetodoPago";
            cmbMetodoPago.Size = new Size(235, 28);
            cmbMetodoPago.TabIndex = 2;
            cmbMetodoPago.SelectedIndexChanged += cmbMetodoPago_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Calisto MT", 10.8F);
            label3.Location = new Point(46, 169);
            label3.Name = "label3";
            label3.Size = new Size(78, 21);
            label3.TabIndex = 3;
            label3.Text = "Subtotal:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Calisto MT", 10.8F);
            label4.Location = new Point(54, 200);
            label4.Name = "label4";
            label4.Size = new Size(49, 21);
            label4.TabIndex = 4;
            label4.Text = "IVA:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label5.Location = new Point(46, 247);
            label5.Name = "label5";
            label5.Size = new Size(77, 28);
            label5.TabIndex = 5;
            label5.Text = "TOTAL:";
            // 
            // lblSubtotal
            // 
            lblSubtotal.AutoSize = true;
            lblSubtotal.Location = new Point(320, 169);
            lblSubtotal.Name = "lblSubtotal";
            lblSubtotal.Size = new Size(44, 20);
            lblSubtotal.TabIndex = 6;
            lblSubtotal.Text = "$0.00";
            // 
            // btnCancelar
            // 
            btnCancelar.Font = new Font("Segoe UI", 10F);
            btnCancelar.Location = new Point(83, 358);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(94, 29);
            btnCancelar.TabIndex = 7;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnRealizarVenta
            // 
            btnRealizarVenta.Font = new Font("Segoe UI", 10F);
            btnRealizarVenta.Location = new Point(236, 358);
            btnRealizarVenta.Name = "btnRealizarVenta";
            btnRealizarVenta.Size = new Size(155, 29);
            btnRealizarVenta.TabIndex = 8;
            btnRealizarVenta.Text = "Realizar Venta";
            btnRealizarVenta.UseVisualStyleBackColor = true;
            btnRealizarVenta.Click += btnRealizarVenta_Click;
            // 
            // lblIVA
            // 
            lblIVA.AutoSize = true;
            lblIVA.Location = new Point(320, 200);
            lblIVA.Name = "lblIVA";
            lblIVA.Size = new Size(44, 20);
            lblIVA.TabIndex = 9;
            lblIVA.Text = "$0.00";
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 14F);
            lblTotal.Location = new Point(307, 247);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(71, 32);
            lblTotal.TabIndex = 10;
            lblTotal.Text = "$0.00";
            // 
            // FrmSeleccionPago
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            CancelButton = btnCancelar;
            ClientSize = new Size(473, 450);
            Controls.Add(lblTotal);
            Controls.Add(lblIVA);
            Controls.Add(btnRealizarVenta);
            Controls.Add(btnCancelar);
            Controls.Add(lblSubtotal);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(cmbMetodoPago);
            Controls.Add(label2);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FrmSeleccionPago";
            Text = "Venta de Boletos";
            Load += FrmSeleccionPago_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private ComboBox cmbMetodoPago;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label lblSubtotal;
        private Button btnCancelar;
        private Button btnRealizarVenta;
        private Label lblIVA;
        private Label lblTotal;
    }
}