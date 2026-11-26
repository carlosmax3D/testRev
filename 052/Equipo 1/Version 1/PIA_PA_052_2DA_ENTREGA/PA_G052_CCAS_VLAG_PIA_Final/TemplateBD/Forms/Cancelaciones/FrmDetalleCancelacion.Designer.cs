namespace TemplateCSharp.Forms.Cancelaciones
{
    partial class FrmDetalleCancelacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDetalleCancelacion));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            Clientselection = new Label();
            partidocompra = new Label();
            fechacompra = new Label();
            totalcompra = new Label();
            label8 = new Label();
            txtMotivo = new TextBox();
            btnConfirmar = new Button();
            btnSalir = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(22, 27);
            label1.Name = "label1";
            label1.Size = new Size(214, 28);
            label1.TabIndex = 0;
            label1.Text = "Cancelacion de Venta";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Calisto MT", 10.2F);
            label2.Location = new Point(22, 75);
            label2.Name = "label2";
            label2.Size = new Size(53, 20);
            label2.TabIndex = 1;
            label2.Text = "Folio:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Calisto MT", 10.2F);
            label3.Location = new Point(73, 75);
            label3.Name = "label3";
            label3.Size = new Size(50, 20);
            label3.TabIndex = 2;
            label3.Text = "0000.";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Calisto MT", 10.2F);
            label4.Location = new Point(22, 110);
            label4.Name = "label4";
            label4.Size = new Size(66, 20);
            label4.TabIndex = 3;
            label4.Text = "Cliente:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Calisto MT", 10.2F);
            label5.Location = new Point(22, 147);
            label5.Name = "label5";
            label5.Size = new Size(66, 20);
            label5.TabIndex = 4;
            label5.Text = "Partido:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Calisto MT", 10.2F);
            label6.Location = new Point(22, 188);
            label6.Name = "label6";
            label6.Size = new Size(139, 20);
            label6.TabIndex = 5;
            label6.Text = "Fecha del partido:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Calisto MT", 10.2F);
            label7.Location = new Point(22, 228);
            label7.Name = "label7";
            label7.Size = new Size(77, 20);
            label7.TabIndex = 6;
            label7.Text = "TOTAL: ";
            // 
            // Clientselection
            // 
            Clientselection.AutoSize = true;
            Clientselection.Font = new Font("Calisto MT", 10.2F);
            Clientselection.Location = new Point(86, 110);
            Clientselection.Name = "Clientselection";
            Clientselection.Size = new Size(163, 20);
            Clientselection.TabIndex = 7;
            Clientselection.Text = "Cliente seleccionado.";
            // 
            // partidocompra
            // 
            partidocompra.AutoSize = true;
            partidocompra.Font = new Font("Calisto MT", 10.2F);
            partidocompra.Location = new Point(87, 147);
            partidocompra.Name = "partidocompra";
            partidocompra.Size = new Size(137, 20);
            partidocompra.TabIndex = 8;
            partidocompra.Text = "Partido en espera.";
            // 
            // fechacompra
            // 
            fechacompra.AutoSize = true;
            fechacompra.Font = new Font("Calisto MT", 10.2F);
            fechacompra.Location = new Point(156, 188);
            fechacompra.Name = "fechacompra";
            fechacompra.Size = new Size(304, 20);
            fechacompra.TabIndex = 9;
            fechacompra.Text = "Fecha del partido de la compra realizada.";
            // 
            // totalcompra
            // 
            totalcompra.AutoSize = true;
            totalcompra.Font = new Font("Calisto MT", 10.2F);
            totalcompra.Location = new Point(87, 228);
            totalcompra.Name = "totalcompra";
            totalcompra.Size = new Size(190, 20);
            totalcompra.TabIndex = 10;
            totalcompra.Text = "Total Final de la compra.";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Calisto MT", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.Location = new Point(22, 309);
            label8.Name = "label8";
            label8.Size = new Size(189, 21);
            label8.TabIndex = 11;
            label8.Text = "Motivo de cancelación:";
            // 
            // txtMotivo
            // 
            txtMotivo.Location = new Point(26, 349);
            txtMotivo.Multiline = true;
            txtMotivo.Name = "txtMotivo";
            txtMotivo.Size = new Size(617, 96);
            txtMotivo.TabIndex = 12;
            txtMotivo.TextChanged += txtMotivo_TextChanged;
            // 
            // btnConfirmar
            // 
            btnConfirmar.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnConfirmar.Location = new Point(64, 490);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(232, 44);
            btnConfirmar.TabIndex = 13;
            btnConfirmar.Text = "Confirmar Cancelacion";
            btnConfirmar.UseVisualStyleBackColor = true;
            btnConfirmar.Click += btnConfirmar_Click;
            // 
            // btnSalir
            // 
            btnSalir.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSalir.Location = new Point(440, 490);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(143, 44);
            btnSalir.TabIndex = 14;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // FrmDetalleCancelacion
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(698, 576);
            Controls.Add(btnSalir);
            Controls.Add(btnConfirmar);
            Controls.Add(txtMotivo);
            Controls.Add(label8);
            Controls.Add(totalcompra);
            Controls.Add(fechacompra);
            Controls.Add(partidocompra);
            Controls.Add(Clientselection);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FrmDetalleCancelacion";
            Text = "Cancelacion Venta";
            Load += FrmDetalleCancelacion_Load;
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
        private Label Clientselection;
        private Label partidocompra;
        private Label fechacompra;
        private Label totalcompra;
        private Label label8;
        private TextBox txtMotivo;
        private Button btnConfirmar;
        private Button btnSalir;
    }
}